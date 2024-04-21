using Bluesoft.Bank.Data.Enums;

namespace Bluesoft.Bank.Data.Entities;
#nullable disable
public class AccountMovement
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public AccountMovementType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedOn { get; set; }
    public int BranchId { get; set; }
    public Guid TransactionCode { get; set; }

    public Account Account { get; set; }
    public Branch Branch { get; set; }
}
