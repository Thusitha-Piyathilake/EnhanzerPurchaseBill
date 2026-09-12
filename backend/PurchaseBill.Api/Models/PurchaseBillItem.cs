namespace PurchaseBill.Api.Models;

public class PurchaseBillItem
{
    public int Id { get; set; }

    public string Item { get; set; } = string.Empty;

    public string Batch { get; set; } = string.Empty;

    public decimal StandardCost { get; set; }

    public decimal StandardPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalCost { get; set; }

    public decimal TotalSelling { get; set; }
}