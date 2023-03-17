namespace Services.AuthorizationAPI.Models.Repository.Interfaces;

public interface IAddressRepository : IBaseRepository<AddressesUser>
{
    Task<List<AddressesUser>> AddressesGet(int userId);
}