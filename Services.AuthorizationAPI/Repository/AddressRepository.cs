using Microsoft.EntityFrameworkCore;
using Services.AuthorizationAPI.Database;

namespace Services.AuthorizationAPI.Models.Repository.Interfaces;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _db;

    public AddressRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public Task<List<AddressesUser>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<AddressesUser> GetById(int id)
    {
        return await _db.AddressesUsers.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AddressesUser> Create(AddressesUser model)
    {
        var address = await _db.AddressesUsers.AddAsync(model);
        await _db.SaveChangesAsync();
        return address.Entity;
    }

    public async Task<AddressesUser> Update(AddressesUser model)
    {
        var address = _db.Update(model);
        await _db.SaveChangesAsync();
        return address.Entity;
    }

    public async Task<bool> Delete(AddressesUser model)
    {
        try
        {
            _db.AddressesUsers.Remove(model);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<List<AddressesUser>> AddressesGet(int userId)
    {
        return await _db.AddressesUsers.Where(a => a.UserId == userId).ToListAsync();
    }
}