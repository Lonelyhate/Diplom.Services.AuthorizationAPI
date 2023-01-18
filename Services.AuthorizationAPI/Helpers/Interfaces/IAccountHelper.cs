using Services.AuthorizationAPI.Models;

namespace Services.AuthorizationAPI.Helpers.Interfaces;

public interface IAccountHelper
{
    void CreatedPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    string CreateToken(User user);
}