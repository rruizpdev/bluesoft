using Bluesoft.Bank.Data.Enums;

namespace Bluesoft.Bank.Data.FunctionResults;
#nullable disable
public class ClientTransactionsCount
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ClientType Type { get; set; }

    public int TransactionsQty { get; set; }
}
