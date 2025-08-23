using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.CreateTransaction;
using TransactionManagement.Core.Transactions.DeleteTransaction;
using TransactionManagement.Core.Transactions.GetTransaction;
using TransactionManagement.Core.Transactions.UpdateTransaction;
using TransactionManagement.Infrastructure.Transactions;

namespace SubscriptionTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --------------------------------------
            // Register MVC and Swagger
            // --------------------------------------
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // --------------------------------------
            // Dependency Injection
            // --------------------------------------

            // Repository (In-Memory Implementation)
            builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

            // Simple Mediator
            builder.Services.AddSingleton<IMediator, SimpleMediator>();

            // Register Handlers for all Commands & Queries with proper Result<T> return types
            builder.Services.AddTransient<IRequestHandler<CreateTransactionCommand, Result<Transaction>>, CreateTransactionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTransactionsByUserQuery, Result<IEnumerable<Transaction>>>, GetTransactionsByUserQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateTransactionCommand, Result<Transaction>>, UpdateTransactionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<DeleteTransactionCommand, Result>, DeleteTransactionCommandHandler>();

            // --------------------------------------
            // App Pipeline
            // --------------------------------------
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
