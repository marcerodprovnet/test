using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Pagination
{
    public class ListQueryHelper
    {
        public Expression<Func<T, object>> GetOrderBy<T>(
            PaginateRequestDto requestDto,
            IDictionary<string, string> sortableFields)
        {
            if (requestDto.SortField == null)
            {
                return null;
            }

            if (!sortableFields.ContainsKey(requestDto.SortField))
            {
                throw new Exception($"Invalid Sort Column '{requestDto.SortField}'");
            }

            var sortPath = sortableFields[requestDto.SortField];

            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = GetMemberExpression(parameterExp, sortPath);

            return Expression.Lambda<Func<T, object>>(BoxValueType(propertyExp), parameterExp);

        }

        public List<Expression<Func<T, bool>>> GetFilters<T>(
            IList<FilterRequestDto> filterRequestDto,
            IDictionary<string, string> filterableFields)
        {
            var filters = new List<Expression<Func<T, bool>>>();
            if (filterRequestDto != null)
            {
                foreach (var filter in filterRequestDto)
                {
                    if (!filterableFields.ContainsKey(filter.Field))
                    {
                        throw new Exception($"Invalid Filter Column '{filter.Field}'");
                    }
                    var fieldPath = filterableFields[filter.Field];
                    var filterExpression = GetFilterExpression<T>(fieldPath, filter.Value, filter.Operation);
                    filters.Add(filterExpression);
                }
            }
            return filters;
        }

        Expression<Func<T, bool>> GetFilterExpression<T>(
            string propertyName,
            string propertyValue,
            FilterOperation? operation)
        {
            Expression predicateExpression;
            var methodName = GetOperation(operation);
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = GetMemberExpression(parameterExp, propertyName);
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));

            if (propertyName.Contains("Date"))
            {
                predicateExpression = CreateDateTimeCompareExpression(propertyValue, operation, propertyExp);
            }
            else
            {
                predicateExpression = CreateStringCompareExpression(operation, propertyExp, method, someValue);
            }

            return Expression.Lambda<Func<T, bool>>(predicateExpression, parameterExp);
        }

        private Expression CreateStringCompareExpression(FilterOperation? operation, MemberExpression propertyExp, System.Reflection.MethodInfo method, ConstantExpression someValue)
        {
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);
            var predicateExpression = IsNegated(operation)
                ? Expression.Not(containsMethodExp)
                : (Expression)containsMethodExp;
            return predicateExpression;
        }

        private Expression CreateDateTimeCompareExpression(string propertyValue, FilterOperation? operation, MemberExpression propertyExp)
        {
            var splitedDate = propertyValue.Split('/');
            var day = int.Parse(splitedDate[1]);
            var month = int.Parse(splitedDate[0]);
            var year = int.Parse(splitedDate[2]);

            NewExpression constructorExpression = GetDateTimeConstructorExpression(day, month, year);
            System.Reflection.MethodInfo compareToMethod = GetDateTimeCompareToMethod();
            MemberExpression dateTimeMemberExpression = GetDateTimeMemberExpression(propertyExp);

            var comparisonExpression = Expression.Call(dateTimeMemberExpression, compareToMethod, constructorExpression);
            var expression = Expression.Equal(comparisonExpression, Expression.Constant(0));
            var predicateExpression = IsNegated(operation)
                ? Expression.Not(expression)
                : (Expression)expression;
            return predicateExpression;
        }

        private static NewExpression GetDateTimeConstructorExpression(int day, int month, int year)
        {
            var constructorInfo = typeof(DateTime).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int) });
            var constructorExpression = Expression.New(constructorInfo, Expression.Constant(year, typeof(Int32)), Expression.Constant(month, typeof(Int32)), Expression.Constant(day, typeof(Int32)));
            return constructorExpression;
        }

        private static System.Reflection.MethodInfo GetDateTimeCompareToMethod()
        {
            return typeof(DateTime).GetMethod("CompareTo", new[] { typeof(DateTime) });
        }

        private static MemberExpression GetDateTimeMemberExpression(MemberExpression propertyExp)
        {
            var dateTimeExpression = Expression.Convert(propertyExp, typeof(DateTime));
            var dateMemberExpression = typeof(DateTime).GetMember("Date");
            var dateTimeMemberExpression = Expression.MakeMemberAccess(dateTimeExpression, dateMemberExpression[0]);
            return dateTimeMemberExpression;
        }

        MemberExpression GetMemberExpression(Expression param, string propertyName)
        {
            if (propertyName.Contains("."))
            {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }

        bool IsNegated(FilterOperation? operartion)
        {
            return operartion == FilterOperation.NotEquals || operartion == FilterOperation.NotContains;
        }

        string GetOperation(FilterOperation? operation)
        {
            if (operation == null)
            {
                return "Contains";
            }

            switch (operation)
            {
                case FilterOperation.Contains:
                case FilterOperation.NotContains:
                    return "Contains";
                case FilterOperation.Equals:
                case FilterOperation.NotEquals:
                    return "Equals";
                case FilterOperation.StartsWith:
                    return "StartsWith";
                case FilterOperation.EndsWith:
                    return "EndsWith";
            }

            throw new Exception($"Operation not implemented '{operation}'");
        }

        Expression BoxValueType(MemberExpression propertyExp)
        {
            //  Lambda does not perform auto-boxing for value types
            if (propertyExp.Type.IsValueType)
            {
                return Expression.Convert(propertyExp, typeof(object));
            }
            return propertyExp;
        }
    }
}
