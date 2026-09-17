namespace PurchaseBill.Api.DTOs;

public class OldestPurchaseOrderItemDto
{
    public int Id { get; set; }

    public int PurchaseOrderId { get; set; }

    public string Item { get; set; } = string.Empty;

    public string Batch { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime PurchaseOrderCreatedAt { get; set; }
}