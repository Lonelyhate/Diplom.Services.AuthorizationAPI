using Microsoft.EntityFrameworkCore;
using Services.AuthorizationAPI.Database;
using Services.AuthorizationAPI.Models.Repository.Interfaces;

namespace Services.AuthorizationAPI.Models.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> Create(User model)
    {
        var user = await _db.Users.AddAsync(model);
        await _db.SaveChangesAsync();
        return user.Entity;
    }

    public Task<User> Update(User model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(User model)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<bool> CheckUserByEmail(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user is not null;
    }
}