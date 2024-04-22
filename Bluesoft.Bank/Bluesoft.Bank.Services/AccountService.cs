using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.Repositories.Contracts;
using Bluesoft.Bank.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluesoft.Bank.Services;

public class AccountService : IAccountService
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
