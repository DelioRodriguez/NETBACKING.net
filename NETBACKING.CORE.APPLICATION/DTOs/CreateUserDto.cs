namespace NETBACKING.CORE.APPLICATION.DTOs
{
    public class CreateUserDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal? InitialAmount { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
    }
}