using Bluesoft.Bank.Data.Enums;

namespace Bluesoft.Bank.Data.Entities;

#nullable disable
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ClientType  Type { get; set; }
    public int AddressId { get; set; }

    public Address Address { get; set; }

    public ICollection<Account> Accounts = new HashSet<Account>();
}
