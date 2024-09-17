namespace Qinshift.Services
{
    public class BankAccount
    {
        private double _balance;

        public BankAccount()
        {
            
        }

        public BankAccount(double balance)
        {
            _balance = balance;
        }

        public double Balance => _balance;

        public void Add(double amount)
        {
            if(amount < 0)
             throw new ArgumentOutOfRangeException(nameof(amount));

            _balance += amount;
        }

        public void Withdraw(double amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            if(amount > _balance)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _balance -= amount;
        }


        public void TransferFundsTo(BankAccount otherAccount, double amount)
        {
            if (otherAccount == null) 
                throw new ArgumentNullException(nameof(otherAccount));

            Withdraw(amount);
            otherAccount.Add(amount);
        }

        public double Sum(double num1, double num2)
        {
            return num1 + num2;
        }
    }
}
