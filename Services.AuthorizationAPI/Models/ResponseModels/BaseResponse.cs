using Services.AuthorizationAPI.Models.Enums;

namespace Services.AuthorizationAPI.Models;

/// <summary>
/// Базовый ответ от сервера
/// </summary>
/// <typeparam name="T">Содержимое ответа</typeparam>
public class BaseResponse<T>
{
    /// <summary>
    /// Ошибка?
    /// </summary>
    public bool isSuccess { get; set; } = true;
    /// <summary>
    /// Статус код
    /// </summary>
    public StatusCode StatusCodes { get; set; }
    /// <summary>
    /// Содержимое
    /// </summary>
    public T Data { get; set; }
    /// <summary>
    /// Отображаемое сообщение
    /// </summary>
    public string DisplayMessage { get; set; }
    /// <summary>
    /// Ошибки
    /// </summary>
    public List<string> ErrorMessage { get; set; }
}