using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}