using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }

    public int? CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails? CustomerDetails { get; set; }
}
