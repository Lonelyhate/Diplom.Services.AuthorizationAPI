using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.RequestModels.Discount;

public class DiscountUpdateRequestModel
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}