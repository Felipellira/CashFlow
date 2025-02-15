using CashFlow.API.Controllers;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess.Repositories;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(
    IWriteOnlyExpensesRepository expensesRepository,
    IUnitOfWork unitOfWork
) : IDeleteExpenseUseCase
{
    public async Task Execute(long id)
    {
        var result = await expensesRepository.RemoveById(id);
        if (result is false) throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        await unitOfWork.Commit();
    }
}