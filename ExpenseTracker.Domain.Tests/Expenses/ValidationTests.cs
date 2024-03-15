using AutoFixture;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Validation;
using FluentAssertions;

namespace ExpenseTracker.Domain.Tests.Expenses
{
    public sealed class ValidationTests
    {
        private static readonly Fixture Fixture = new();

        private static readonly ExpenseId CorrectId = ExpenseId.Create(0).Value;

        private static readonly Recipient MinCorrectRecipient = Recipient.Create(string.Join(string.Empty, Fixture.CreateMany<char>(2))).Value;
        private static readonly Recipient MaxCorrectRecipient = Recipient.Create(string.Join(string.Empty, Fixture.CreateMany<char>(255))).Value;

        private static readonly Money MinCorrectSpending = Money.Create(1, "CHF").Value;
        private static readonly Money MaxCorrectSpending = Money.Create(1_000_000, "USD").Value;

        private static readonly TransactionDate MinCorrectTransactionDate = TransactionDate.Create(DateTime.MinValue.AddDays(1)).Value;
        private static readonly TransactionDate MaxCorrectTransactionDate = TransactionDate.Create(DateTime.Today).Value;

        private static readonly ExpenseType CorrectType = ExpenseType.Create("Drinks").Value;

        [Test]
        public void GivenIncorrectIdParameter_WhenExpenseIdIsCreated_FailureResultReturned()
        {
            // Act 
            var result = ExpenseId.Create(Fixture.Create<int>() * -1);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.ExpenseId.ErrorCodes.ValueIsInvalid());
        }

        [Test]
        public void GivenCorrectIdParameter_WhenExpenseIdIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = ExpenseId.Create(CorrectId.Value);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptyRecipientParameter_WhenRecipientIsCreated_FailureResultReturned(string? recipient)
        {
            // Act 
            var result = Recipient.Create(recipient!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Recipient.ErrorCodes.NameIsRequired());
        }

        [Test]
        public void GivenTooShortRecipientParameter_WhenRecipientIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Recipient.Create(MinCorrectRecipient.Name[..^1]);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Recipient.ErrorCodes.LengthIsInvalid());
        }

        [Test]
        public void GivenTooLongRecipientParameter_WhenRecipientIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Recipient.Create(MaxCorrectRecipient.Name + "A");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Recipient.ErrorCodes.LengthIsInvalid());
        }

        [Test]
        public void GivenCorrectRecipientParameter_WhenRecipientIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = Recipient.Create(MaxCorrectRecipient.Name);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GivenTooLowAmountParameter_WhenMoneyIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Money.Create(MinCorrectSpending.Amount - 1, MinCorrectSpending.Currency);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Money.ErrorCodes.AmountIsInvalid());
        }

        [Test]
        public void GivenTooHighAmountParameter_WhenMoneyIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Money.Create(MaxCorrectSpending.Amount + 1, MaxCorrectSpending.Currency);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Money.ErrorCodes.AmountIsInvalid());
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptyCurrencyParameter_WhenMoneyIsCreated_FailureResultReturned(string? spendingCurrency)
        {
            // Act 
            var result = Money.Create(MaxCorrectSpending.Amount, spendingCurrency!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Money.ErrorCodes.CurrencyIsRequired());
        }

        [Test]
        public void GivenIncorrectCurrencyParameter_WhenMoneyIsCreated_FailureResultReturned()
        {
            // Act 
            var result = Money.Create(MaxCorrectSpending.Amount, Fixture.Create<string>());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.Money.ErrorCodes.CurrencyIsInvalid());
        }

        [TestCase("CHF")]
        [TestCase("EUR")]
        [TestCase("USD")]
        public void GivenCorrectAmountAndCurrencyParameters_WhenMoneyIsCreated_SuccessResultReturned(string currency)
        {
            // Act 
            var result = Money.Create(MaxCorrectSpending.Amount, currency);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GivenTooEarlyTransactionDateParameter_WhenTransactionDateIsCreated_FailureResultReturned()
        {
            // Act 
            var result = TransactionDate.Create(MinCorrectTransactionDate.Value.AddDays(-1));

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.TransactionDate.ErrorCodes.ValueIsRequired());
        }

        [Test]
        public void GivenTooLateTransactionDateParameter_WhenTransactionDateIsCreated_FailureResultReturned()
        {
            // Act 
            var result = TransactionDate.Create(MaxCorrectTransactionDate.Value.AddDays(1));

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.TransactionDate.ErrorCodes.ValueIsInvalid());
        }

        [Test]
        public void GivenCorrectTransactionDateParameter_WhenTransactionDateIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = TransactionDate.Create(MaxCorrectTransactionDate.Value);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenEmptyTypeParameter_WhenExpenseTypeIsCreated_FailureResultReturned(string? type)
        {
            // Act 
            var result = ExpenseType.Create(type!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.ExpenseType.ErrorCodes.ValueIsRequired());
        }

        [Test]
        public void GivenIncorrectTypeParameter_WhenExpenseTypeIsCreated_FailureResultReturned()
        {
            // Act 
            var result = ExpenseType.Create(Fixture.Create<string>());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Count.Should().Be(1);
            result.Error.First().Should().Be(Domain.Expenses.Validation.ExpenseType.ErrorCodes.ValueIsInvalid());
        }

        [TestCase("Drinks")]
        [TestCase("Food")]
        [TestCase("Other")]
        public void GivenCorrectTypeParameter_WhenExpenseTypeIsCreated_SuccessResultReturned(string type)
        {
            // Act 
            var result = ExpenseType.Create(type);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturned()
        {
            // Act 
            var result = Expense.Create(CorrectId, MinCorrectRecipient, MinCorrectSpending, MinCorrectTransactionDate, CorrectType);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}