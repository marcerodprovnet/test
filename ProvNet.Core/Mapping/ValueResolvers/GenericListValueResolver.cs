using AutoMapper;
using ProvNet.Core.Dto;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Mapping.ValueResolvers
{
    public class GenericListValueResolver<TChildDto, TChildEntity> : IMemberValueResolver<object, object, IList<TChildDto>, IList<TChildEntity>>
            where TChildEntity : EntityWithId<int>
            where TChildDto : BaseDtoWithId<int>
    {

        public IList<TChildEntity> Resolve(object source, object destination, IList<TChildDto> sourceMember, IList<TChildEntity> destMember, ResolutionContext context)
        {
            if (destMember == null)
            {
                destMember = new List<TChildEntity>();
            }

            AddOrUpdateEntities(destMember, sourceMember, context);
            DeleteEntities(destMember, sourceMember);
            return new List<TChildEntity>(destMember);
        }

        private void DeleteEntities(IList<TChildEntity> entities, IList<TChildDto> dtos)
        {
            var entitiesToRemove = new List<TChildEntity>();
            foreach (var entity in entities.Where(x => x.Id != 0))
            {
                var existingDto = dtos.FirstOrDefault(x => x.Id.Equals(entity.Id));
                if (existingDto == null)
                {
                    entitiesToRemove.Add(entity);
                }
            }

            foreach (var entityToRemove in entitiesToRemove)
            {
                entities.Remove(entityToRemove);
            }
        }

        private void AddOrUpdateEntities(IList<TChildEntity> entities, IList<TChildDto> dtos, ResolutionContext context)
        {
            foreach (var dto in dtos)
            {
                var existingEntity = entities.Where(x => x.Id != 0).FirstOrDefault(x => x.Id.Equals(dto.Id));
                if (existingEntity != null)
                {
                    context.Mapper.Map(dto, existingEntity);
                }
                else
                {
                    var newEntity = context.Mapper.Map<TChildEntity>(dto);
                    entities.Add(newEntity);
                }
            }
        }
    }
}
