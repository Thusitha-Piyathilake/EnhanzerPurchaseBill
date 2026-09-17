namespace PurchaseBill.Api.Models;

public class PurchaseOrder
{
    public int Id { get; set; }

    public decimal NetAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<PurchaseBillItem> Items { get; set; }
        = new List<PurchaseBillItem>();
}