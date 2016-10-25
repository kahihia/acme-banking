using System;

namespace Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string Description { get; set; }
        public string SpendingCategory { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TransactionTypes
    {
        Debit,
        Deposit,
        Transfer
    }
}