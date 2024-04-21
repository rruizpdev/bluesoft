using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Repositories.Contracts;
using RCRP.Common.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Bluesoft.Bank.Data.Repositories;

public class ClientRepository
    : RepositoryBase<Client>
    , IClientRepository
{
    public ClientRepository(BluesoftBankContext dbContext)
        : base(dbContext, dbContext.Set<Client>())
    {
    }

    protected override Expression<Func<Client, bool>> FilterByKeyPredicate(
        params object[] keyValues)
        => t => t.Id == (int)keyValues[0];
}
