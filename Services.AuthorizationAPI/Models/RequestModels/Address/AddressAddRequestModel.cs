using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.RequestModels.Address;

public class AddressAddRequestModel
{
    [JsonPropertyName("address")]
    public string Address { get; set; }
}