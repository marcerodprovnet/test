namespace ProvNet.Core.Authentication.Interfaces
{
    public interface IUserData
    {
        public int TenantId { get; set; }
        public string TenantCode { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

    }
}
