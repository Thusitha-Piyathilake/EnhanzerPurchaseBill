namespace PurchaseBill.Api.DTOs;

public class ExternalLoginResponse
{
    public int Status_Code { get; set; }

    public string Sync_Time { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public List<ExternalUser> Response_Body { get; set; } = [];
}