using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Amount, f => f.Random.Decimal(1, 10000))
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());
    }
}