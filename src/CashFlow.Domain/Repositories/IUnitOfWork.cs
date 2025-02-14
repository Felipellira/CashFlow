namespace CashFlow.Infrastructure.DataAccess.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}