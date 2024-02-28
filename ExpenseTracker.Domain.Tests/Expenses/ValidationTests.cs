using AutoFixture;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Validation;
using FluentAssertions;

namespace ExpenseTracker.Domain.Tests.Expenses
{
    public sealed class ValidationTests
    {
        private const int ID = 0;

        private string _incorrectType = string.Empty;

        private string _minCorrectRecipient = string.Empty;
        private string _maxCorrectRecipient = string.Empty;

        private decimal _minCorrectSpendingAmount;
        private decimal _maxCorrectSpendingAmount;

        private string _incorrectSpendingCurrency = string.Empty;

        private DateTime _minCorrectTransactionDate;
        private DateTime _maxCorrectTransactionDate;

        private readonly string[] _correctSpendingCurrencies = ["CHF", "EUR", "USD"];

        private readonly string[] _correctTypes = ["Drinks", "Food", "Other"];

        private readonly Fixture _fixture = new();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //Arrange
            _incorrectType = _fixture.Create<string>();

            _minCorrectRecipient = string.Join(string.Empty, _fixture.CreateMany<char>(2));
            _maxCorrectRecipient = string.Join(string.Empty, _fixture.CreateMany<char>(255));

            _minCorrectSpendingAmount = 1;
            _maxCorrectSpendingAmount = 1_000_000;

            _incorrectSpendingCurrency = _fixture.Create<string>();

            _minCorrectTransactionDate = DateTime.MinValue.AddDays(1);
            _maxCorrectTransactionDate = DateTime.Today;
        }

        [Test]
        public void GivenCorrectMinimumParameters_WhenExpenseIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _minCorrectRecipient, _minCorrectSpendingAmount, _correctSpendingCurrencies[0], _minCorrectTransactionDate, _correctTypes[0]);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectMaximumParameters_WhenExpenseIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptyTypeParameter_WhenExpenseIsCreated_FailureResultReturned(string? type)
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, type!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.ExpenseType.ValueIsRequired());
        }

        [Test]
        public void GivenIncorrectTypeParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, _incorrectType);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.ExpenseType.ValueIsInvalid());
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptyRecipientParameter_WhenExpenseIsCreated_FailureResultReturned(string? recipient)
        {
            // Act 
            var result = Expense.Create(ID, recipient!, _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Recipient.ValueIsRequired());
        }

        [Test]
        public void GivenTooShortRecipientParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _minCorrectRecipient[..^1], _minCorrectSpendingAmount, _correctSpendingCurrencies[0], _minCorrectTransactionDate, _correctTypes[0]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Recipient.LengthIsInvalid());
        }

        [Test]
        public void GivenTooLongRecipientParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient + "A", _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Recipient.LengthIsInvalid());
        }

        [Test]
        public void GivenTooLowSpendingAmountParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _minCorrectRecipient, _minCorrectSpendingAmount - 1, _correctSpendingCurrencies[0], _minCorrectTransactionDate, _correctTypes[0]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Spending.AmountIsInvalid());
        }

        [Test]
        public void GivenTooHighSpendingAmountParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount + 1, _correctSpendingCurrencies[2], _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Spending.AmountIsInvalid());
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptySpendingCurrencyParameter_WhenExpenseIsCreated_FailureResultReturned(string? spendingCurrency)
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, spendingCurrency!, _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Spending.CurrencyIsRequired());
        }

        [Test]
        public void GivenIncorrectSpendingCurrencyParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, _incorrectSpendingCurrency, _maxCorrectTransactionDate, _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.Spending.CurrencyIsInvalid());
        }

        [Test]
        public void GivenTooEarlyTransactionDateParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _minCorrectRecipient, _minCorrectSpendingAmount, _correctSpendingCurrencies[0], _minCorrectTransactionDate.AddDays(-1), _correctTypes[0]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.TransactionDate.ValueIsRequired());
        }

        [Test]
        public void GivenTooLateTransactionDateParameter_WhenExpenseIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Expense.Create(ID, _maxCorrectRecipient, _maxCorrectSpendingAmount, _correctSpendingCurrencies[2], _maxCorrectTransactionDate.AddDays(1), _correctTypes[2]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().Be(Errors.TransactionDate.ValueIsInvalid());
        }
    }
}