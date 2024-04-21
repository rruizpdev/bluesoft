using Bluesoft.Bank.Data.Contexts;
using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Repositories.Contracts;
using RCRP.Common.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Bluesoft.Bank.Data.Repositories;

public class AddressRepository
    : RepositoryBase<Address>
    , IAddressRepository
{
    public AddressRepository(BluesoftBankContext dbContext)
        : base(dbContext, dbContext.Set<Address>())
    {
    }

    protected override Expression<Func<Address, bool>> FilterByKeyPredicate(
        params object[] keyValues)
        => t => t.Id == (int)keyValues[0];
}
