namespace Api.Models
{
    /// <summary>
    /// Account Model
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public AccountTypes AccountType { get; set; }
        public decimal Balance { get; set; }
    }

    /// <summary>
    /// Account types
    /// </summary>
    public enum AccountTypes
    {
        BusinessChecking,
        PersonalChecking,
        PersonalSavings
    }
}