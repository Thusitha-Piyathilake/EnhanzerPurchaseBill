using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Services;

public interface ILocationDetailService
{
    Task<List<LocationDetail>> GetAllAsync();

    Task<LocationDetail?> GetByIdAsync(int id);

    Task<LocationDetail> CreateAsync(LocationDetail location);

    Task<bool> UpdateAsync(int id, LocationDetail location);

    Task<bool> DeleteAsync(int id);
}