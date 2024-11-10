namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Products
{
    public class ProductCreateViewModel
    {
        public string ProductType { get; set; } 
        
        public string UniqueIdentifier { get; set; }

        public decimal? Balance { get; set; }
        
        public bool IsPrimary { get; set; }

        public decimal? CreditLimit { get; set; }
        
        public decimal? LoanAmount { get; set; }

        public string ApplicationUserId { get; set; }
    }
}