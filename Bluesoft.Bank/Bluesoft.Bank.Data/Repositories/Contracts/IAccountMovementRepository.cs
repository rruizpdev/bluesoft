using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.FunctionResults;
using RCRP.Common.DataAccess.Repositories.Contracts;

namespace Bluesoft.Bank.Data.Repositories.Contracts;

public interface IAccountMovementRepository
    : IRepository<AccountMovement>
{
    Task<IEnumerable<ClientTransactionsCount>> GetClientTransactionsCountInRange(
        (DateTime From, DateTime To) range);
}
