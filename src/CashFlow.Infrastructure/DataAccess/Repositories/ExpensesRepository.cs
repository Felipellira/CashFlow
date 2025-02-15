using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext)
    : IReadOnlyExpensesRepository, IWriteOnlyExpensesRepository, IUpdateOnlyExpensesRepository
{
    private readonly CashFlowDbContext _dbContext = dbContext;

    public async Task<List<Expense>> GetAll()
    {
        var expenses = await _dbContext.Expenses.AsNoTracking().ToListAsync();
        return expenses;
    }

    async Task<Expense?> IReadOnlyExpensesRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Expense>> GetByMonth(DateOnly month)
    {
        return await _dbContext.Expenses.AsNoTracking().Where(
            e => e.Date.Month == month.Month && e.Date.Year == month.Year
        ).OrderBy(expense => expense.Date).ToListAsync();
    }

    async Task<Expense?> IUpdateOnlyExpensesRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }


    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> RemoveById(long id)
    {
        var foundExpense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (foundExpense is null) return false;

        _dbContext.Expenses.Remove(foundExpense);
        return true;
    }
}