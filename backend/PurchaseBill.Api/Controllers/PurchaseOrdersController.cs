using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Data;
using PurchaseBill.Api.DTOs;
using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseOrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseOrdersController(ApplicationDbContext context)
    {
        _context = context;
    }


    // ==========================================
    // GET: api/PurchaseOrders
    // Get all Purchase Orders
    // ==========================================

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
    {
        var purchaseOrders = await _context.PurchaseOrders
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(purchaseOrders);
    }


    // ==========================================
    // GET: api/PurchaseOrders/{id}
    // Get one Purchase Order
    // ==========================================

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
    {
        var purchaseOrder = await _context.PurchaseOrders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (purchaseOrder == null)
        {
            return NotFound(new
            {
                message = "Purchase Order not found."
            });
        }

        return Ok(purchaseOrder);
    }


    // ==========================================
    // POST: api/PurchaseOrders
    // Create a Purchase Order
    // ==========================================

    [HttpPost]
    public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder(
        PurchaseOrderCreateRequest request)
    {
        if (request.ItemIds == null || request.ItemIds.Count == 0)
        {
            return BadRequest(new
            {
                message = "At least one item is required to create a Purchase Order."
            });
        }

        // Remove duplicate item IDs
        var itemIds = request.ItemIds
            .Distinct()
            .ToList();

        // Find the existing Purchase Bill Items
        var items = await _context.PurchaseBillItems
            .Where(x => itemIds.Contains(x.Id))
            .ToListAsync();

        // Make sure all requested items exist
        if (items.Count != itemIds.Count)
        {
            return BadRequest(new
            {
                message = "One or more selected items could not be found."
            });
        }

        // Make sure these items are not already attached
        // to another Purchase Order
        if (items.Any(x => x.PurchaseOrderId != null))
        {
            return BadRequest(new
            {
                message = "One or more selected items already belong to a Purchase Order."
            });
        }

        // Calculate the net amount from the item total costs
        var netAmount = items.Sum(x => x.TotalCost);

        // Create the Purchase Order
        var purchaseOrder = new PurchaseOrder
        {
            NetAmount = netAmount,
            CreatedAt = DateTime.Now
        };

        _context.PurchaseOrders.Add(purchaseOrder);

        // Save first so the Purchase Order gets its ID
        await _context.SaveChangesAsync();

        // Attach the selected items to the new Purchase Order
        foreach (var item in items)
        {
            item.PurchaseOrderId = purchaseOrder.Id;
        }

        await _context.SaveChangesAsync();

        // Load the items so they are included in the response
        await _context.Entry(purchaseOrder)
            .Collection(x => x.Items)
            .LoadAsync();

        return CreatedAtAction(
            nameof(GetPurchaseOrder),
            new { id = purchaseOrder.Id },
            purchaseOrder);
    }
}