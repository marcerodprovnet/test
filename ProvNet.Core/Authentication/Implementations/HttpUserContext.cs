using ProvNet.Core.Authentication.Interfaces;

namespace ProvNet.Core.Authentication.Implementations
{
    public class HttpUserContext : IUserContext
    {
        public IUserData User
        {
            get
            {
                return new UserData();
            }
        }
    }
}
