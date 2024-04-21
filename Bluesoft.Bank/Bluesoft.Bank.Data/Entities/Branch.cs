namespace Bluesoft.Bank.Data.Entities;

#nullable disable
public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int AddressId { get; set; }
    public Address Address { get; set; }
}

