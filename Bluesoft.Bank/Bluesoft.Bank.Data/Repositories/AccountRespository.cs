using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Repositories.Contracts;
using RCRP.Common.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Bluesoft.Bank.Data.Repositories;

public class AccountRespository
    : RepositoryBase<Account>
    , IAccountRespository
{
    public AccountRespository(BluesoftBankContext dbContext)
        : base(dbContext, dbContext.Set<Account>())
    {
    }

    protected override Expression<Func<Account,bool>> FilterByKeyPredicate(
        params object[] keyValues)
         => t => t.Id == (int)keyValues[0];
}
