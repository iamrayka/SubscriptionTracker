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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
            builder.Services.AddTransient<CreateTransactionHandler>();
            builder.Services.AddTransient<GetTransactionsByUserHandler>();
            builder.Services.AddTransient<UpdateTransactionHandler>();
            builder.Services.AddTransient<DeleteTransactionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
