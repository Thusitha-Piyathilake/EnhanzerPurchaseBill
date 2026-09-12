namespace PurchaseBill.Api.DTOs;

public class ExternalLocation
{
    public string Location_Code { get; set; } = string.Empty;

    public string Location_Name { get; set; } = string.Empty;

    public int Stock_Handle { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public int Status { get; set; }
}