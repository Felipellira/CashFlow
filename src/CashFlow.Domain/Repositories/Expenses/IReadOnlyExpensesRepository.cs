using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IReadOnlyExpensesRepository
{
    Task<List<Expense>> GetAll();

    Task<Expense?> GetById(long id);
}