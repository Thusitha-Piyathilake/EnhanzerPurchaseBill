using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Data;
using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseBillItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseBillItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/PurchaseBillItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseBillItem>>> GetPurchaseBillItems()
    {
        return await _context.PurchaseBillItems.ToListAsync();
    }

    // GET: api/PurchaseBillItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseBillItem>> GetPurchaseBillItem(int id)
    {
        var item = await _context.PurchaseBillItems.FindAsync(id);

        if (item == null)
        {
            return NotFound(new
            {
                message = "Purchase bill item not found"
            });
        }

        return Ok(item);
    }

    // POST: api/PurchaseBillItems
    [HttpPost]
    public async Task<ActionResult<PurchaseBillItem>> CreatePurchaseBillItem(
        PurchaseBillItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Item))
        {
            return BadRequest(new
            {
                message = "Item is required"
            });
        }

        if (string.IsNullOrWhiteSpace(item.Batch))
        {
            return BadRequest(new
            {
                message = "Batch is required"
            });
        }

        if (item.Quantity <= 0)
        {
            return BadRequest(new
            {
                message = "Quantity must be greater than 0"
            });
        }

        if (item.StandardCost < 0)
        {
            return BadRequest(new
            {
                message = "Standard Cost cannot be negative"
            });
        }

        if (item.StandardPrice < 0)
        {
            return BadRequest(new
            {
                message = "Standard Price cannot be negative"
            });
        }

        if (item.Discount < 0 || item.Discount > 100)
        {
            return BadRequest(new
            {
                message = "Discount must be between 0 and 100"
            });
        }

        // Calculate Total Cost
        item.TotalCost =
            (item.StandardCost * item.Quantity)
            - ((item.StandardCost * item.Quantity) * item.Discount / 100);

        // Calculate Total Selling
        item.TotalSelling =
            item.StandardPrice * item.Quantity;

        _context.PurchaseBillItems.Add(item);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPurchaseBillItem),
            new { id = item.Id },
            item);
    }

    // PUT: api/PurchaseBillItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePurchaseBillItem(
        int id,
        PurchaseBillItem item)
    {
        if (id != item.Id)
        {
            return BadRequest(new
            {
                message = "ID mismatch"
            });
        }

        var existingItem =
            await _context.PurchaseBillItems.FindAsync(id);

        if (existingItem == null)
        {
            return NotFound(new
            {
                message = "Purchase bill item not found"
            });
        }

        if (string.IsNullOrWhiteSpace(item.Item))
        {
            return BadRequest(new
            {
                message = "Item is required"
            });
        }

        if (string.IsNullOrWhiteSpace(item.Batch))
        {
            return BadRequest(new
            {
                message = "Batch is required"
            });
        }

        if (item.Quantity <= 0)
        {
            return BadRequest(new
            {
                message = "Quantity must be greater than 0"
            });
        }

        if (item.StandardCost < 0)
        {
            return BadRequest(new
            {
                message = "Standard Cost cannot be negative"
            });
        }

        if (item.StandardPrice < 0)
        {
            return BadRequest(new
            {
                message = "Standard Price cannot be negative"
            });
        }

        if (item.Discount < 0 || item.Discount > 100)
        {
            return BadRequest(new
            {
                message = "Discount must be between 0 and 100"
            });
        }

        existingItem.Item = item.Item;
        existingItem.Batch = item.Batch;
        existingItem.StandardCost = item.StandardCost;
        existingItem.StandardPrice = item.StandardPrice;
        existingItem.Quantity = item.Quantity;
        existingItem.Discount = item.Discount;

        existingItem.TotalCost =
            (item.StandardCost * item.Quantity)
            - ((item.StandardCost * item.Quantity) * item.Discount / 100);

        existingItem.TotalSelling =
            item.StandardPrice * item.Quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/PurchaseBillItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchaseBillItem(int id)
    {
        var item =
            await _context.PurchaseBillItems.FindAsync(id);

        if (item == null)
        {
            return NotFound(new
            {
                message = "Purchase bill item not found"
            });
        }

        _context.PurchaseBillItems.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}