namespace NETBACKING.CORE.APPLICATION.DTOs
{
    public class LoginResultDTO
    {
        public bool IsSuccessful { get; set; }
        public string UserRole { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsActive { get; set; }
        public string RedirectUrl { get; set; }
    }
}