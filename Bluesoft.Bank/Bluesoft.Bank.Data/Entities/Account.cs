using Bluesoft.Bank.Data.Enums;

namespace Bluesoft.Bank.Data.Entities;

#nullable disable
public class Account
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int BranchId { get; set; }
    public int ClientId { get; set; }

    public bool Active { get; set; } = false;
    public bool Blocked { get; set; } = false;

    public DateTime CreatedOn { get; set; }

    public AccountType Type { get; set; }

    public decimal Balance { get; set; } = 0M;

    public Client Client { get; set; }
    public Branch Branch { get; set; }

    public ICollection<AccountMovement> Movements { get; set; }
}

