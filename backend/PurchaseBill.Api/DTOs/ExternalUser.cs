namespace PurchaseBill.Api.DTOs;

public class ExternalUser
{
    public string User_Code { get; set; } = string.Empty;

    public string User_Display_Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string User_Employee_Code { get; set; } = string.Empty;

    public string Company_Code { get; set; } = string.Empty;

    public List<ExternalLocation> User_Locations { get; set; } = [];
}