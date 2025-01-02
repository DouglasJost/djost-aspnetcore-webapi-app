using AppDomainEntities;
using System;
using System.Threading.Tasks;

namespace AppServiceCore.Services.DbTransactionService
{
    public interface IDbTransactionService
    {
        Task<DbOperationResult<T>> ExecuteWithTransactionAsync<T> (MusicCollectionDbContext dbContext, Func<Task<T>> operation);
    }
}
