using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.FunctionResults;
using Bluesoft.Bank.Data.Repositories.Contracts;
using Bluesoft.Bank.Services.Contracts;
using RCRP.Common.Helpers;

namespace Bluesoft.Bank.Services;

public class AccountMovementService
    : IAccountMovementService
{
    private readonly IAccountMovementRepository _accountMovementRepository;
    public AccountMovementService(IAccountMovementRepository accountMovementRepository)
    {
        _accountMovementRepository = accountMovementRepository; 
    }

    public async Task<IEnumerable<ClientTransactionsCount>> GetClientMonthTransactionsCountAsync(
        int month,
        int year)
    {
        var range = DateTimeHelpers.BuildMonthRange(month, year);
        return await _accountMovementRepository.GetClientTransactionsCountInRange(range);
    }

    public async Task<IEnumerable<Client>> GetClientsWithOtherCitiesWitdrawalsAsync(
        int month,
        int year)
    {
        var range = DateTimeHelpers.BuildMonthRange(month, year);

        return null;
    }
}
