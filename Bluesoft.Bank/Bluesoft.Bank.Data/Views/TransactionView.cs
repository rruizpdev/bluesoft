namespace Bluesoft.Bank.Data.Views;
#nullable disable
public class TransactionView
{
    public int TransactionId { get; set; }
    public int ClientId { get; set; }
    public int AccountCityId { get; set; }
    public int TransactionCityId { get; set; }
    public int AccountId { get; set; }
    public string AccountNumber { get; set; }
    public Guid TransactionCode { get; set; }
    public int TransactionTypeId { get; set; }
    public string TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal SignedAmount { get; set; }
}
