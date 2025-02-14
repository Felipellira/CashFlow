using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

internal class GetExpenseByIdUseCase(
    IExpensesRepository expensesRepository,
    IMapper mapper
) : IGetExpenseByIdUseCase
{
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await expensesRepository.GetById(id);

        if (result is null) throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);

        return mapper.Map<ResponseExpenseJson>(result);
    }
}