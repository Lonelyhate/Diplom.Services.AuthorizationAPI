using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.ViewModels;

public class AddressViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("address")]
    public string Address { get; set; }
    
    [JsonPropertyName("isActiveAddress")]
    public int IsActiveAddress { get; set; }
}