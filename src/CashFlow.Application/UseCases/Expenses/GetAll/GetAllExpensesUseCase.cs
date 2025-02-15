using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

internal class GetAllExpensesUseCase(IReadOnlyExpensesRepository expensesRepository, IMapper mapper)
    : IGetAllExpensesUseCase
{
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await expensesRepository.GetAll();
        return new ResponseExpensesJson
        {
            Expenses = mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}