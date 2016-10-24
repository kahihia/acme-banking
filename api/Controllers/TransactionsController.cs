using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Transaction API resource
    /// </summary>
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ApiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionsController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Account transactions resource.
        /// </summary>
        /// <param name="accountId">int, AccountId</param>
        /// <param name="limit">int, optional, default is 10</param>
        /// <param name="offset">int, optional, default is 0</param>
        /// <returns>Collection of transactions by account ID.</returns>
        [HttpGet()]
        public IEnumerable<Transaction> GetTransactions(int accountId, int limit = 10, int offset = 0)
        {
            var transactions = _context.Transactions.Where(t => t.AccountId == accountId).Skip(offset).Take(limit);

            return transactions;
        }
    }
}