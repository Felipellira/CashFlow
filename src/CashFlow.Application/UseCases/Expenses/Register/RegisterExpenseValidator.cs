using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_REQUIRED);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(x => x.Date).LessThan(DateTime.UtcNow).WithMessage(ResourceErrorMessage.EXPENSES_CANNOT_FOR_THE_FUTURE);
        RuleFor(x => x.PaymentType).IsInEnum().WithMessage(ResourceErrorMessage.PAYMENT_TYPE_INVALID);
    }
}