using Bluesoft.Bank.Data.FunctionResults;

namespace Bluesoft.Bank.Services.Contracts;

public interface IAccountMovementService
{
    Task<IEnumerable<ClientTransactionsCount>> GetClientMonthTransactionsCountAsync(
        int month,
        int year);
}
