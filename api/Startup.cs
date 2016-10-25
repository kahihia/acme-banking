using System;
using System.Linq;
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
            SeedDatabase(context);

            app.UseCors(builder => builder.WithOrigins("http://localhost:8100"));

            app.UseMvc();
        }

        /// <summary>
        /// Seed in-memroy database with data.
        /// </summary>
        /// <param name="context">ApiContext</param>
        private void SeedDatabase(ApiContext context)
        {
            // Add accounts
            context.Accounts.Add(new Account
            {
                AccountNumber = "1111111111111111111111111111111111",
                AccountType = AccountTypes.PersonalChecking,
                Balance = 0.0M
            });

            context.Accounts.Add(new Account
            {
                AccountNumber = "2222222222222222222222222222222222",
                AccountType = AccountTypes.PersonalSavings,
                Balance = 0.0M
            });

            // Add some transactions
            var random = new Random();

            for (int i = 0; i < 364; i++)
            {
                // Make a deposit on the 15th and the 30th.
                var depositOrDebit = DateTime.Now.AddDays(-i).Day % 15;

                context.Transactions.Add(new Transaction
                {
                    AccountId = 1,
                    Date = DateTime.Now.AddDays(-i),
                    TransactionType = depositOrDebit == 0 ? TransactionTypes.Deposit : TransactionTypes.Debit,
                    Description = depositOrDebit == 0 ? "Deposit" : "Debit",
                    SpendingCategory = depositOrDebit == 0 ? "" : GetRandomeSpendingCategory(random),
                    Amount = depositOrDebit == 0 ? 3200.00M : GetRandomDecimal(random, -100, -1)
                });

                // Tranfer 10% to savings on the 15th and the 30th.
                if (depositOrDebit == 0)
                {
                    context.Transactions.Add(new Transaction
                    {
                        AccountId = 1,
                        Date = DateTime.Now.AddDays(-i),
                        TransactionType = TransactionTypes.Transfer,
                        Description = "Transfer to savings",
                        Amount = -320.00M
                    });

                    context.Transactions.Add(new Transaction
                    {
                        AccountId = 2,
                        Date = DateTime.Now.AddDays(-i),
                        TransactionType = TransactionTypes.Deposit,
                        Description = "Deposit",
                        Amount = 320.00M
                    });
                }
            }

            context.SaveChanges();

            // Update account balances.
            var checkingAccount = context.Accounts.Single(a => a.Id == 1);
            checkingAccount.Balance = context.Transactions.Where(a => a.AccountId == 1).Sum(a => a.Amount);

            var savingsAccount = context.Accounts.Single(a => a.Id == 2);
            savingsAccount.Balance = context.Transactions.Where(a => a.AccountId == 2).Sum(a => a.Amount);

            context.SaveChanges();
        }

        /// <summary>
        /// Get a random deciaml for seed transactions.
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns></returns>
        private decimal GetRandomDecimal(Random random, int min, int max)
        {
            return Math.Round(random.Next(min, max) + (decimal)random.NextDouble(), 2);
        }

        /// <summary>
        /// Get a random spending category for deposits.
        /// </summary>
        /// <returns>A random spending category.</returns>
        private string GetRandomeSpendingCategory(Random random)
        {
            var spendingCategories = new string[] { "Groceries", "Fuel", "Entertainment", "Utilities", "Membership", "Travel", "Medical" };

            var randomSpendingCategory = spendingCategories[random.Next(0, spendingCategories.Length)];

            return randomSpendingCategory;
        }
    }
}
