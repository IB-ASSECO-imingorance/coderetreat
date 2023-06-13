using System;
using System.Collections.Generic;
using System.Text;
using Tests;

namespace Kata {
    public class Account {
        public const string FIELD_SEPARATOR = "\t";
        private IDictionary<DateTime, int> balance = new Dictionary<DateTime, int>();
        private int amount;
        private IDateTimeRepository dateTimeRepository;

        public Account(IDateTimeRepository dateTimeRepository) {
            this.dateTimeRepository = dateTimeRepository;
        }

        public Account() : this(DateTimeRepository.Instance) {
        }

        public int Amount { get => amount; }

        public void Deposit(int amount) {
            this.amount += amount;
            this.balance.Add(dateTimeRepository.GetCurrentDateTime(), amount);
        }

        internal void Withdraw(int amount) {
            this.amount -= amount;
            this.balance.Add(dateTimeRepository.GetCurrentDateTime(), -amount);
        }

        internal string PrintStatement() {
            string header = "Date" + FIELD_SEPARATOR + "Amount" + FIELD_SEPARATOR + "Balance";
            StringBuilder statement = new StringBuilder();
            int currentAmount = 0;

            statement.AppendLine(header);
            foreach (KeyValuePair<DateTime, int> pair in balance) {
                currentAmount += pair.Value;
                statement.AppendLine(pair.Key.ToString("dd.M.yyyy") + FIELD_SEPARATOR + pair.Value.ToString("+#;-#") + FIELD_SEPARATOR + currentAmount);
            }
            return statement.ToString();
        }

    }
}
