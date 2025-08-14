using System;
using System.Collections.Generic;

namespace FinanceManagement
{
    public enum TransactionType { Deposit, Withdrawal, Transfer }

    public record Transaction(
        decimal Amount,
        TransactionType Type,
        DateTime Date,
        string Description = "",
        string? ToAccountNumber = null
    );

    public class Account
    {
        public string AccountNumber { get; }
        public string Owner { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, string owner, decimal openingBalance = 0m)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = openingBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public override string ToString() => $"{Owner} ({AccountNumber}) - Balance: {Balance:C}";
    }

    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, string owner, decimal openingBalance = 0m)
            : base(accountNumber, owner, openingBalance) { }

        public override void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                base.Withdraw(amount);
                Console.WriteLine($"Withdrawal successful. New Balance: {Balance:C}");
            }
        }
    }

    public interface ITransactionProcessor
    {
        void Process(Account account, Transaction transaction);
    }

    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Account account, Transaction transaction)
        {
            Console.WriteLine($"Processing bank transfer of {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Account account, Transaction transaction)
        {
            Console.WriteLine($"Processing mobile money payment of {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Account account, Transaction transaction)
        {
            Console.WriteLine($"Processing crypto payment of {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class FinanceApp
    {
        private List<Transaction> _transactions = new();

        public void Run()
        {
            var savings = new SavingsAccount("002", "Student", 1000m);

            var t1 = new Transaction(100m, TransactionType.Deposit, DateTime.Now, "Groceries");
            var t2 = new Transaction(200m, TransactionType.Withdrawal, DateTime.Now, "Utilities");
            var t3 = new Transaction(50m, TransactionType.Withdrawal, DateTime.Now, "Entertainment");

            var processors = new Dictionary<TransactionType, ITransactionProcessor>
            {
                { TransactionType.Deposit, new MobileMoneyProcessor() },
                { TransactionType.Withdrawal, new BankTransferProcessor() },
                { TransactionType.Transfer, new CryptoWalletProcessor() }
            };

            processors[t1.Type].Process(savings, t1);
            savings.Deposit(t1.Amount);

            processors[t2.Type].Process(savings, t2);
            savings.Withdraw(t2.Amount);

            processors[t3.Type].Process(savings, t3);
            savings.Withdraw(t3.Amount);

            _transactions.AddRange(new[] { t1, t2, t3 });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var app = new FinanceApp();
            app.Run();
        }
    }
}
