using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.FunctionResults;

namespace Bluesoft.Bank.Services.Contracts;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAsync();
}
