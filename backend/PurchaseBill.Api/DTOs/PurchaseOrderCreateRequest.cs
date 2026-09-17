namespace PurchaseBill.Api.DTOs;

public class PurchaseOrderCreateRequest
{
    public List<int> ItemIds { get; set; } = new();
}