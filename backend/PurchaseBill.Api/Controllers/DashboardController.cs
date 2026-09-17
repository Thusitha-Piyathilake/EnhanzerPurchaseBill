using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Data;
using PurchaseBill.Api.DTOs;

namespace PurchaseBill.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ==========================================
    // GET: api/Dashboard/latest-orders
    // Latest 5 Purchase Orders
    // ==========================================

    [HttpGet("latest-orders")]
    public async Task<ActionResult<IEnumerable<LatestPurchaseOrderDto>>>
        GetLatestOrders()
    {
        var orders = await _context.PurchaseOrders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(5)
            .Select(x => new LatestPurchaseOrderDto
            {
                Id = x.Id,
                NetAmount = x.NetAmount,
                NoOfItems = x.Items.Count(),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return Ok(orders);
    }

    // ==========================================
    // GET: api/Dashboard/oldest-items
    // Oldest 10 items from saved Purchase Orders
    // ==========================================

    [HttpGet("oldest-items")]
    public async Task<ActionResult<IEnumerable<OldestPurchaseOrderItemDto>>>
        GetOldestItems()
    {
        var items = await _context.PurchaseBillItems
            .AsNoTracking()
            .Where(x => x.PurchaseOrderId != null)
            .Join(
                _context.PurchaseOrders,
                item => item.PurchaseOrderId,
                order => order.Id,
                (item, order) => new OldestPurchaseOrderItemDto
                {
                    Id = item.Id,
                    PurchaseOrderId = order.Id,
                    Item = item.Item,
                    Batch = item.Batch,
                    Quantity = item.Quantity,
                    TotalCost = item.TotalCost,
                    PurchaseOrderCreatedAt = order.CreatedAt
                })
            .OrderBy(x => x.PurchaseOrderCreatedAt)
            .ThenBy(x => x.Id)
            .Take(10)
            .ToListAsync();

        return Ok(items);
    }

    // ==========================================
    // GET: api/Dashboard/item-quantities
    // Quantity grouped by item
    // ==========================================

    [HttpGet("item-quantities")]
    public async Task<ActionResult<IEnumerable<ItemQuantityDto>>>
        GetItemQuantities()
    {
        var itemQuantities = await _context.PurchaseBillItems
            .AsNoTracking()
            .Where(x => x.PurchaseOrderId != null)
            .GroupBy(x => x.Item)
            .Select(group => new ItemQuantityDto
            {
                ItemName = group.Key,
                Quantity = group.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.Quantity)
            .ToListAsync();

        return Ok(itemQuantities);
    }
}