using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.AuthorizationAPI.Models;

/// <summary>
/// Таблица Пользователи
/// </summary>
[Table("Users")]
public class User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required]
    [Column("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// Хэшированный пароль
    /// </summary>
    [Required]
    [Column("password_hash")]
    public byte[] PasswordHash { get; set; }
    
    /// <summary>
    /// Соль для пароля
    /// </summary>
    [Required]
    [Column("password_salt")]
    public byte[] PasswordSalt { get; set; }
}