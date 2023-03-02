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

    public async Task<User> GetById(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> Create(User model)
    {
        var user = await _db.Users.AddAsync(model);
        await _db.SaveChangesAsync();
        return user.Entity;
    }

    public async Task<User> Update(User model)
    {
        var user = _db.Users.Update(model);
        await _db.SaveChangesAsync();
        return user.Entity;
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