using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.ViewModels;

public class DiscountViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    /// <summary>
    /// Номер карты
    /// </summary>
    [JsonPropertyName("numberCard")]
    public string NumberCard { get; set; }
    
    /// <summary>
    /// Сумма покупок
    /// </summary>
    [JsonPropertyName("amountPurchases")]
    public decimal AmountPurchases { get; set; }
    
    /// <summary>
    /// Размер скидки
    /// </summary>
    [JsonPropertyName("sizeDscount")]
    public int SizeDiscount { get; set; }
    
    /// <summary>
    /// Сумма до скидки
    /// </summary>
    [JsonPropertyName("amountBeforeDiscount")]
    public int AmountBeforeDiscount { get; set; }
}