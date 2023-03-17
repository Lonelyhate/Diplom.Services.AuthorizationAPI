using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Services.AuthorizationAPI.Models.ViewModels;

public class UserViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    [EmailAddress(ErrorMessage = "Некорректная почта")]
    [StringLength(25, MinimumLength = 6, ErrorMessage = "Длинна не должна быть меньше 6 и больше 25")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("firstname")]
    public string? Firstname { get; set; } = "";
    
    [JsonPropertyName("lastname")]
    public string? Lastname { get; set; } = "";
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; } = "";

    /// <summary>
    /// Токен пользователя
    /// </summary>
    public string? token { get; set; }
}