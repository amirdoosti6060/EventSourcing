using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class BankAccount
    {
        private readonly List<object> _changes = new List<object>();
        private decimal _balance;

        public BankAccount(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public decimal Balance => _balance;

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            _changes.Add(new FundsDepositedEvent { Amount = amount });
            _balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            if (amount > _balance)
                throw new InvalidOperationException("Insufficient funds");

            _changes.Add(new FundsWithdrawnEvent { Amount = amount });
            _balance -= amount;
        }

        public List<object> GetChanges() => _changes;
        public void ClearChanges() => _changes.Clear();

        public static BankAccount LoadFromHistory(Guid id, IEnumerable<object> history)
        {
            var account = new BankAccount(id);
            foreach (var @event in history)
            {
                account.ApplyChange(@event);
            }
            return account;
        }

        private void ApplyChange(object @event)
        {
            switch (@event)
            {
                case FundsDepositedEvent deposited:
                    _balance += deposited.Amount;
                    break;
                case FundsWithdrawnEvent withdrawn:
                    _balance -= withdrawn.Amount;
                    break;
            }
        }
    }
}
