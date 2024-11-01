using Microsoft.AspNetCore.Identity;
using NETBACKING.CORE.DOMAIN.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Identification { get; set; }
    public bool IsActive { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InitialAmount { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}