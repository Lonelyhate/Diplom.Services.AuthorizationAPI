using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.RequestModels.Address;

public class AddressUpdateRequestModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; } 
        
    [JsonPropertyName("address")]
    public string Address { get; set; }
}