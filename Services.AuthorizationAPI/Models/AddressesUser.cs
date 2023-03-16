using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.AuthorizationAPI.Models;

[Table("Addresses_User")]
public class AddressesUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    [Column("address")]
    public string Address { get; set; }
}