using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using RCRP.Common.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Bluesoft.Bank.Data.Repositories;

public class BranchRepository
    : RepositoryBase<Account>
{
    public BranchRepository(BluesoftBankContext dbContext)
        : base(dbContext, dbContext.Set<Account>())
    {
    }

    protected override Expression<Func<Account, bool>> FilterByKeyPredicate(
        params object[] keyValues)
        => t => t.Id == (int)keyValues[0];
}
