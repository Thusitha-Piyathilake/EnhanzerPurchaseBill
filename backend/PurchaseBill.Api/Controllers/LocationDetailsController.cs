using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Data;
using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LocationDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/LocationDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDetail>>> GetLocationDetails()
    {
        return await _context.LocationDetails.ToListAsync();
    }

    // GET: api/LocationDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDetail>> GetLocationDetail(int id)
    {
        var location = await _context.LocationDetails.FindAsync(id);

        if (location == null)
        {
            return NotFound(new
            {
                message = "Location not found"
            });
        }

        return Ok(location);
    }

    // POST: api/LocationDetails
    [HttpPost]
    public async Task<ActionResult<LocationDetail>> CreateLocationDetail(
        LocationDetail location)
    {
        if (string.IsNullOrWhiteSpace(location.LocationCode))
        {
            return BadRequest(new
            {
                message = "LocationCode is required"
            });
        }

        if (string.IsNullOrWhiteSpace(location.LocationName))
        {
            return BadRequest(new
            {
                message = "LocationName is required"
            });
        }

        _context.LocationDetails.Add(location);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLocationDetail),
            new { id = location.Id },
            location);
    }

    // POST: api/LocationDetails/sync
    [HttpPost("sync")]
    public async Task<IActionResult> SyncLocations(
        List<LocationDetail> locations)
    {
        if (locations == null || locations.Count == 0)
        {
            return BadRequest(new
            {
                message = "No locations were provided."
            });
        }

        foreach (var location in locations)
        {
            if (string.IsNullOrWhiteSpace(location.LocationCode) ||
                string.IsNullOrWhiteSpace(location.LocationName))
            {
                continue;
            }

            var locationCode = location.LocationCode.Trim();
            var locationName = location.LocationName.Trim();

            var existingLocation =
                await _context.LocationDetails
                    .FirstOrDefaultAsync(
                        x => x.LocationCode == locationCode);

            if (existingLocation == null)
            {
                _context.LocationDetails.Add(
                    new LocationDetail
                    {
                        LocationCode = locationCode,
                        LocationName = locationName
                    });
            }
            else
            {
                existingLocation.LocationName = locationName;
            }
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Locations synchronized successfully."
        });
    }

    // PUT: api/LocationDetails/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocationDetail(
        int id,
        LocationDetail location)
    {
        // The ID comes from the URL.
        // Angular sends only LocationCode and LocationName,
        // so we do not require location.Id in the request body.
        if (location.Id != 0 && id != location.Id)
        {
            return BadRequest(new
            {
                message = "ID mismatch"
            });
        }

        var existingLocation =
            await _context.LocationDetails.FindAsync(id);

        if (existingLocation == null)
        {
            return NotFound(new
            {
                message = "Location not found"
            });
        }

        if (string.IsNullOrWhiteSpace(location.LocationCode))
        {
            return BadRequest(new
            {
                message = "LocationCode is required"
            });
        }

        if (string.IsNullOrWhiteSpace(location.LocationName))
        {
            return BadRequest(new
            {
                message = "LocationName is required"
            });
        }

        existingLocation.LocationCode =
            location.LocationCode.Trim();

        existingLocation.LocationName =
            location.LocationName.Trim();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/LocationDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocationDetail(int id)
    {
        var location =
            await _context.LocationDetails.FindAsync(id);

        if (location == null)
        {
            return NotFound(new
            {
                message = "Location not found"
            });
        }

        _context.LocationDetails.Remove(location);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}