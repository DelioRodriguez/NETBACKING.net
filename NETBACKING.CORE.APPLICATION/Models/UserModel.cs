using NETBACKING.CORE.APPLICATION.Enums;

namespace NETBACKING.CORE.APPLICATION.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}