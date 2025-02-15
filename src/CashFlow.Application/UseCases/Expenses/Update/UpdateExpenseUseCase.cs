using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess.Repositories;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(
    IUpdateOnlyExpensesRepository expensesRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IUpdateExpenseUseCase
{
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await expensesRepository.GetById(id);

        if (expense is null) throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);

        mapper.Map(request, expense);

        expensesRepository.Update(expense);

        await unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}