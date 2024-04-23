using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.FunctionResults;
using Bluesoft.Bank.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using RCRP.Common.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Bluesoft.Bank.Data.Repositories;
#nullable disable
public class AccountMovementRepository
    : RepositoryBase<AccountMovement>
    , IAccountMovementRepository
{
    public AccountMovementRepository(BluesoftBankContext dbContext)
        : base(dbContext, dbContext.Set<AccountMovement>())
    {
    }

    public async Task<IEnumerable<ClientTransactionsCount>> GetClientTransactionsCountInRange(
        (DateTime From, DateTime To) range)
    {
        var query = (_context as BluesoftBankContext).FnClientTransactionCountInRange(
            range.From, range.To).OrderByDescending(c => c.TransactionsQty);

        return await query.ToListAsync();
    }

    protected override Expression<Func<AccountMovement, bool>> FilterByKeyPredicate(params object[] keyValues)
    => t => t.Id == (int)keyValues[0];
}
