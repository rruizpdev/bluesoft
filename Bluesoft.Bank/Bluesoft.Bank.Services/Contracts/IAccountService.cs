using Bluesoft.Bank.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluesoft.Bank.Services.Contracts;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAsync();
}
