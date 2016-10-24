using System;
using Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);

            app.UseCors(builder => builder.WithOrigins("http://localhost:8100"));

            app.UseMvc();
        }

        private void AddTestData(ApiContext context)
        {
            // Add accounts
            context.Accounts.Add(new Account
            {
                AccountNumber = "123456789009876543211234567890OTTF",
                AccountType = AccountTypes.PersonalChecking
            });

            context.Accounts.Add(new Account
            {
                AccountNumber = "123456789009876543211234567890FSSE",
                AccountType = AccountTypes.PersonalSavings
            });

            // Add some transactions
            Random random = new Random();

            for (int i = 0; i < 364; i++)
            {
                // Make a deposit on the 15th and the 30th.
                var depositOrDebit = DateTime.Now.AddDays(-i).Day % 15;

                context.Transactions.Add(new Transaction
                {
                    AccountId = 1,
                    Date = DateTime.Now.AddDays(-i),
                    TransactionType = depositOrDebit == 0 ? TransactionTypes.Deposit : TransactionTypes.Debit,
                    Description = depositOrDebit == 0 ? $"Deposit" : $"Debit",
                    Amount = depositOrDebit == 0 ? 3200.00M : random.Next(-100, -1)
                });
            }

            for (int i = 0; i < 364; i++)
            {
                // Make a savings transfer on the 15th and the 30th.
                var depositOrDebit = DateTime.Now.AddDays(-i).Day % 15;

                if (depositOrDebit == 0)
                {
                    context.Transactions.Add(new Transaction
                    {
                        AccountId = 2,
                        Date = DateTime.Now.AddDays(-i),
                        TransactionType = TransactionTypes.Deposit,
                        Description = "Deposit",
                        Amount = 320.00M
                    });

                    context.Transactions.Add(new Transaction
                    {
                        AccountId = 1,
                        Date = DateTime.Now.AddDays(-i),
                        TransactionType = TransactionTypes.Transfer,
                        Description = "Transfer to savings",
                        Amount = 320.00M
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
