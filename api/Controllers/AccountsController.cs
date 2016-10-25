using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Account API resource
    /// For our URL pattern, we'll use {reource}/{identifier}?{queryparams}
    /// </summary>
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ApiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountsController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET Accounts resource.
        /// </summary>
        /// <param name="limit">int, optional, default is 10</param>
        /// <param name="offset">int, optional, default is 0</param>
        /// <returns>Collection of accounts</returns>
        [HttpGet]
        public IEnumerable<Account> GetAccounts(int limit = 10, int offset = 0)
        {
            var accounts = _context.Accounts.Select(a => a);

            return accounts;
        }

        /// <summary>
        /// Get account
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <returns>An Account</returns>
        [HttpGet("{accountId}")]
        public Account GetAccount(int accountId)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.Id == accountId);

            return account;
        }
    }
}