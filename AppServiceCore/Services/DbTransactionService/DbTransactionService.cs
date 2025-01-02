using AppDomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AppServiceCore.Services.DbTransactionService
{
    public class DbTransactionService : IDbTransactionService
    {
        public async Task<DbOperationResult<T>> ExecuteWithTransactionAsync<T>(
                MusicCollectionDbContext dbContext, 
                Func<Task<T>> operation)
        {
            await using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await operation();
                    await transaction.CommitAsync();

                    return new DbOperationResult<T> { Success = true, Data = result };
                }
                catch (DbUpdateException dbUpdateEx)
                {
                    var exMsg = ExceptionUtilities.AppendExceptionMessages(dbUpdateEx);

                    await transaction.RollbackAsync();

                    return new DbOperationResult<T>
                    {
                        Success = false,
                        ErrorMessage = $"Database error: {exMsg}"
                    };
                }
                catch (InvalidOperationException invalidOperationEx)
                {
                    var exMsg = ExceptionUtilities.AppendExceptionMessages(invalidOperationEx);

                    await transaction.RollbackAsync();

                    return new DbOperationResult<T>
                    {
                        Success = false,
                        ErrorMessage = $"Invalid operation: {exMsg}"
                    };
                }
                catch (Exception ex)
                {
                    var exMsg = ExceptionUtilities.AppendExceptionMessages(ex);

                    await transaction.RollbackAsync();
                    return new DbOperationResult<T> { Success = false, ErrorMessage = $"Unexpected error: {exMsg}" };
                }
            }
        }
    }
}
