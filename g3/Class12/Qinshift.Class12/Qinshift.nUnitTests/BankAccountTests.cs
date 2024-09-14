using Qinshift.Services;

namespace Qinshift.nUnitTests
{
    public class Tests
    {
        private BankAccount _bankAccount;
        [SetUp]
        public void Setup()
        {
            _bankAccount = new BankAccount(1000);
        }

        [Test]
        public void BankAccount_AddAmountGreaterThanZero_ShouldIncreaseBalance()
        {
            // 2. Act
            _bankAccount.Add(500);

            // 3. Assert
            //Assert.AreEqual(1500, _bankAccount.Balance);
            Assert.That(_bankAccount.Balance, Is.EqualTo(1500));
        }

        [Test]
        public void BankAccount_AddNegativeAmount_ShouldThrowException()
        {
            // 2. Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Add(-500));
        }

        [Test]
        public void BankAccount_WithdrawAmountLessThanBalance_ShouldDecreaseBalance()
        {
            _bankAccount.Withdraw(300);

            Assert.AreEqual(700, _bankAccount.Balance);
        }

        [Test]
        public void BankAccount_WithdrawAmountLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Withdraw(-200));
        }

        [Test]
        public void BankAccount_WithdrawAmountGreaterThanBalance_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Withdraw(1200));
        }


        [Test]
        public void BankAccount_TransferFundsToOtherExistingAccount_ShouldIncreaseOtherAccountsBalance()
        {
            var otherAccount = new BankAccount();

            _bankAccount.TransferFundsTo(otherAccount, 700);

            Assert.AreEqual(300, _bankAccount.Balance);
            Assert.AreEqual(700, otherAccount.Balance);
        }


        [Test]
        public void BankAccount_TransferFundsToOtherNullAccount_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _bankAccount.TransferFundsTo(null, 500));
        }
    }
}