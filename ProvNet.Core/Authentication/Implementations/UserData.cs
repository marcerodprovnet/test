using ProvNet.Core.Authentication.Interfaces;

namespace ProvNet.Core.Authentication.Implementations
{
    public class UserData : IUserData
    {
        public int TenantId { get; set; }
        public string TenantCode { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int? ClientId { get; set; }
    }
}
