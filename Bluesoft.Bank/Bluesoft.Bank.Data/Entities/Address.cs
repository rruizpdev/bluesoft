namespace Bluesoft.Bank.Data.Entities;
#nullable disable

/// <summary>
/// Estandarizes the addres in the whole system.
/// </summary>
public class Address
{
    public int Id { get; set; }
    public string Street1 { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public City City { get; set; }
}
