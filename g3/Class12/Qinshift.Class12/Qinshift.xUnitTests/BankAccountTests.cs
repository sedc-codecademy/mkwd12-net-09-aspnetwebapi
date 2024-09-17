using Qinshift.Services;

namespace Qinshift.xUnitTests
{
    public class BankAccountTests
    {
        private BankAccount _bankAccount;
        public BankAccountTests()
        {
            _bankAccount = new BankAccount(1000);
        }

        [Fact]
        public void BankAccount_AddAmountGreaterThanZero_ShouldIncreaseBalance()
        {
            // 2. Act
            _bankAccount.Add(500);

            // 3. Assert
            Assert.Equal(1500, _bankAccount.Balance);
        }

        [Fact]
        public void BankAccount_AddNegativeAmount_ShouldThrowException()
        {
            // 2. Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Add(-500));
        }

        [Fact]
        public void BankAccount_WithdrawAmountLessThanBalance_ShouldDecreaseBalance()
        {
            _bankAccount.Withdraw(300);

            Assert.Equal(700, _bankAccount.Balance);
        }

        [Fact]
        public void BankAccount_WithdrawAmountLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Withdraw(-200));
        }

        [Fact]
        public void BankAccount_WithdrawAmountGreaterThanBalance_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Withdraw(1200));
        }


        [Fact]
        public void BankAccount_TransferFundsToOtherExistingAccount_ShouldIncreaseOtherAccountsBalance()
        {
            var otherAccount = new BankAccount();

            _bankAccount.TransferFundsTo(otherAccount, 700);

            Assert.Equal(300, _bankAccount.Balance);
            Assert.Equal(700, otherAccount.Balance);
        }


        [Fact]
        public void BankAccount_TransferFundsToOtherNullAccount_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _bankAccount.TransferFundsTo(null, 500));
        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData(30, 50)]
        [InlineData(10, 2)]
        [InlineData(11, 20)]
        [InlineData(15, 20)]
        public void BankAccount_Sum_ShouldSumTwoNumbers(double num1, double num2)
        {
            var result = _bankAccount.Sum(num1, num2);
            Assert.Equal(num1 + num2, result);
        }
    }
}