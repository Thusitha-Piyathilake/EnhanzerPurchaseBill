namespace PurchaseBill.Api.DTOs;

public class LatestPurchaseOrderDto
{
    public int Id { get; set; }

    public decimal NetAmount { get; set; }

    public int NoOfItems { get; set; }

    public DateTime CreatedAt { get; set; }
}