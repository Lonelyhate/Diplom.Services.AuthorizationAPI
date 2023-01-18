namespace Services.AuthorizationAPI.Models.Repository.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email);

    Task<bool> CheckUserByEmail(string email);
}