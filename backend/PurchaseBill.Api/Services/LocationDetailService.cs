using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Data;
using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Services;

public class LocationDetailService : ILocationDetailService
{
    private readonly ApplicationDbContext _context;

    public LocationDetailService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LocationDetail>> GetAllAsync()
    {
        return await _context.LocationDetails
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LocationDetail?> GetByIdAsync(int id)
    {
        return await _context.LocationDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LocationDetail> CreateAsync(LocationDetail location)
    {
        _context.LocationDetails.Add(location);
        await _context.SaveChangesAsync();

        return location;
    }

    public async Task<bool> UpdateAsync(int id, LocationDetail location)
    {
        var existing = await _context.LocationDetails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
        {
            return false;
        }

        existing.LocationCode = location.LocationCode;
        existing.LocationName = location.LocationName;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.LocationDetails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
        {
            return false;
        }

        _context.LocationDetails.Remove(existing);

        await _context.SaveChangesAsync();

        return true;
    }
}