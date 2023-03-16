using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models.RequestModels;

public class RegistrationRequestModel
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    [EmailAddress(ErrorMessage = "Некорректная почта")]
    [StringLength(25, MinimumLength = 6, ErrorMessage = "Длинна не должна быть меньше 6 и больше 25")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [StringLength(35, MinimumLength = 6, ErrorMessage = "Пароль должен содежрить минимум 6 символов")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("password_confrim")]
    [Required (ErrorMessage = "Введите подтвержденный пароль")]
    [StringLength(35, MinimumLength = 6, ErrorMessage = "Пароль должен содежрить минимум 6 символов")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfrim { get; set; }
}