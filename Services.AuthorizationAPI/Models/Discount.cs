using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.AuthorizationAPI.Models;

[Table("Discounts")]
public class Discount
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    /// <summary>
    /// Номер карты
    /// </summary>
    [Column("number_card")]
    public string NumberCard { get; set; }
    
    /// <summary>
    /// Сумма покупок
    /// </summary>
    [Column("amount_purchases")]
    public decimal AmountPurchases { get; set; }
    
    /// <summary>
    /// Размер скидки
    /// </summary>
    [Column("size_discount")]
    public int SizeDiscount { get; set; }
    
    /// <summary>
    /// Сумма до скидки
    /// </summary>
    [Column("amount_before_discount")]
    public int AmountBeforeDiscount { get; set; }
}