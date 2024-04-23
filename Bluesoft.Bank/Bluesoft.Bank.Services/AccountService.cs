using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Repositories.Contracts;
using Bluesoft.Bank.Services.Contracts;

namespace Bluesoft.Bank.Services;

public class AccountService 
    : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    public AccountService(
        IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<Account>> GetAsync()
    {
        return await _accountRepository.GetAsync();
    }

    
}
