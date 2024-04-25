using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Enums;
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

    public async Task<IEnumerable<Client>> GetClientsWithOtherCitiesWitdrawalsAsync(
        (DateTime From, DateTime To) range,
        decimal threshold)
    {
        var context = _context as BluesoftBankContext;
        var clients = context.Transactions.Where(
            t => t.TransactionCityId != t.AccountCityId
            && t.TransactionDate >= range.From 
            && t.TransactionDate <= range.To)
            .DistinctBy(t=> t.ClientId).Select(t => t.ClientId);

        var clientWithdrawals = context.Transactions.Where(
            t => t.TransactionTypeId == AccountMovementType.Withdrawal
                && t.TransactionDate >= range.From
                && t.TransactionDate <= range.To
                && t.Amount > threshold)
            .GroupBy(t => t.ClientId).Select(g => new
            {
                ClientId = g.Key,
                WithdrawalsTotal = g.Sum(w => w.Amount)
            });

        return await context.Clients.Where(c => clients.Contains(c.Id)).ToListAsync();
    }

    protected override Expression<Func<AccountMovement, bool>> FilterByKeyPredicate(params object[] keyValues)
    => t => t.Id == (int)keyValues[0];
}
