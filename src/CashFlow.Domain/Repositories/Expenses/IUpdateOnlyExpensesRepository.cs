using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IUpdateOnlyExpensesRepository
{
    public Task<Expense?> GetById(long id);
    public void Update(Expense expense);
}