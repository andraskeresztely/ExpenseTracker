using AutoFixture;
using ExpenseTracker.Domain.Expenses;
using FluentAssertions;

namespace ExpenseTracker.Domain.Tests.Expenses
{
    public sealed class UsageTests
    {
        private const int ID = 0;
        private string _correctRecipient = string.Empty;
        private decimal _correctSpendingAmount;
        private string _correctSpendingCurrency = string.Empty;
        private DateTime _correctTransactionDate;
        private string _correctType = string.Empty;

        private readonly Fixture _fixture = new();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //Arrange
            _correctRecipient = string.Join(string.Empty, _fixture.CreateMany<char>(2));
            _correctSpendingAmount = 1;
            _correctSpendingCurrency = "CHF";
            _correctTransactionDate = DateTime.Today;
            _correctType = "Food";
        }

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndGettersReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(ID, _correctRecipient, _correctSpendingAmount, _correctSpendingCurrency, _correctTransactionDate, _correctType);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Id.Should().Be(ID);
            result.Value.Recipient.Should().Be(_correctRecipient);
            result.Value.Spending.Amount.Should().Be(_correctSpendingAmount);
            result.Value.Spending.Currency.Should().Be(_correctSpendingCurrency);
            result.Value.TransactionDate.Should().Be(_correctTransactionDate);
            result.Value.Type.Value.Should().Be(_correctType);

            result.Value.Type.ToString().Should().Be(_correctType);
        }

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndToStringsReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(ID, _correctRecipient, _correctSpendingAmount, _correctSpendingCurrency, _correctTransactionDate, _correctType);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Type.ToString().Should().Be(_correctType);
            result.Value.Spending.ToString().Should().Be($"{_correctSpendingCurrency} {_correctSpendingAmount}");
        }
    }
}