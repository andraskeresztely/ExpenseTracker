using AutoFixture;
using ExpenseTracker.Domain.Expenses;
using FluentAssertions;

namespace ExpenseTracker.Domain.Tests.Expenses
{
    public sealed class UsageTests
    {
        private static readonly Fixture Fixture = new();

        private static readonly ExpenseId CorrectId = ExpenseId.Create(0).Value;
        private static readonly Recipient CorrectRecipient = Recipient.Create(string.Join(string.Empty, Fixture.CreateMany<char>(2))).Value;
        private static readonly Money CorrectSpending = Money.Create(1, "CHF").Value;
        private static readonly TransactionDate CorrectTransactionDate = TransactionDate.Create(DateTime.Today).Value;
        private static readonly ExpenseType CorrectType = ExpenseType.Create("Food").Value;

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndGettersReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(CorrectId, CorrectRecipient, CorrectSpending, CorrectTransactionDate, CorrectType);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Id.Value.Should().Be(CorrectId.Value);
            result.Value.Recipient.Name.Should().Be(CorrectRecipient.Name);
            result.Value.Spending.Amount.Should().Be(CorrectSpending.Amount);
            result.Value.Spending.Currency.Should().Be(CorrectSpending.Currency);
            result.Value.TransactionDate.Value.Should().Be(CorrectTransactionDate.Value);
            result.Value.Type.Value.Should().Be(CorrectType.Value);
        }

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndToStringsReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(CorrectId, CorrectRecipient, CorrectSpending, CorrectTransactionDate, CorrectType);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Id.ToString().Should().Be(CorrectId.Value.ToString());
            result.Value.Recipient.ToString().Should().Be(CorrectRecipient.Name);
            result.Value.Spending.ToString().Should().Be($"{CorrectSpending.Currency} {CorrectSpending.Amount}");
            result.Value.TransactionDate.ToString().Should().Be(CorrectTransactionDate.Value.ToString("dd/MM/yyyy"));
            result.Value.Type.ToString().Should().Be(CorrectType.Value);
        }

        [Test]
        public void GivenExpenseId_WhenCastToInt_CorrectValueReturned()
        {
            // Act
            var result = (int)CorrectId;

            // Assert
            result.Should().Be(CorrectId.Value);
        }

        [Test]
        public void GivenTransactionDate_WhenCastToDateTime_CorrectValueReturned()
        {
            // Act
            var result = (DateTime)CorrectTransactionDate;

            // Assert
            result.Should().Be(CorrectTransactionDate.Value);
        }

        [Test]
        public void GivenTwoIdenticalMoneyInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctSpendingCopy = Money.Create(CorrectSpending.Amount, CorrectSpending.Currency).Value;

            // Act
            var result = CorrectSpending == correctSpendingCopy;

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void GivenTwoIdenticalExpenseTypeInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctTypeCopy = ExpenseType.Create(CorrectType.Value).Value;

            // Act
            var result = CorrectType == correctTypeCopy;

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void GivenTwoIdenticalTransactionDateInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctTransactionDateCopy = TransactionDate.Create(CorrectTransactionDate.Value).Value;

            // Act
            var result = CorrectTransactionDate == correctTransactionDateCopy;

            // Assert
            result.Should().BeTrue();
        }
    }
}