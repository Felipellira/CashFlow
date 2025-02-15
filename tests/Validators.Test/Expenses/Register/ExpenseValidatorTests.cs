using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities;
using FluentAssertions;

namespace Validators.Test.Expenses.Register;

public class ExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("         ")]
    public void Error_Title_Empty(string title)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_InTheFuture()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        request.Date = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }

    [Fact]
    public void Error_PaymentType_Invalid()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)999;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}