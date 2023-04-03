using Microsoft.EntityFrameworkCore;
using Services.AuthorizationAPI.Database;
using Services.AuthorizationAPI.Models;
using Services.AuthorizationAPI.Models.Repository.Interfaces;

namespace Services.AuthorizationAPI.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _db;
    
    public DiscountRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public Task<List<Discount>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Discount> GetById(int userId)
    {
        return await _db.Discounts.FirstOrDefaultAsync(d => d.UserId == userId);
    }

    public async Task<Discount> Create(Discount model)
    {
        var discount = await _db.Discounts.AddAsync(model);
        await _db.SaveChangesAsync();
        return discount.Entity;
    }

    public async Task<Discount> Update(Discount model)
    {
        var discount = _db.Discounts.Update(model);
        await _db.SaveChangesAsync();
        return discount.Entity;
    }

    public Task<bool> Delete(Discount model)
    {
        throw new NotImplementedException();
    }
}