using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IWriteOnlyExpensesRepository
{
    Task Add(Expense expense);
    Task<bool> RemoveById(long id);
}