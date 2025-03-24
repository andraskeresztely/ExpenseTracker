using AutoFixture;
using ExpenseTracker.Domain.Expenses;
using Shouldly;

namespace ExpenseTracker.Domain.Tests.Expenses
{
    public sealed class UsageTests
    {
        private static readonly Fixture Fixture = new();

        private static readonly ExpenseId CorrectId = ExpenseId.Create(0).Value;
        private static readonly Recipient CorrectRecipient = Recipient.Create(string.Join(string.Empty, Fixture.CreateMany<char>(2))).Value;
        private static readonly Money CorrectSpending = Money.Create(1, "CHF").Value;
        private static readonly Money CorrectSpendingDifferingByAmount = Money.Create(2, "CHF").Value;
        private static readonly Money CorrectSpendingDifferingByCurrency = Money.Create(1, "USD").Value;
        private static readonly TransactionDate CorrectTransactionDate = TransactionDate.Create(DateTime.Today).Value;
        private static readonly TransactionDate AnotherCorrectTransactionDate = TransactionDate.Create(DateTime.Today.AddDays(-1)).Value;
        private static readonly ExpenseType CorrectType = ExpenseType.Create("Food").Value;
        private static readonly ExpenseType AnotherCorrectType = ExpenseType.Create("Drinks").Value;

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndGettersReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(CorrectId, CorrectRecipient, CorrectSpending, CorrectTransactionDate, CorrectType);

            // Assert
            result.IsSuccess.ShouldBeTrue();

            result.Value.Id.Value.ShouldBe(CorrectId.Value);
            result.Value.Recipient.Name.ShouldBe(CorrectRecipient.Name);
            result.Value.Spending.Amount.ShouldBe(CorrectSpending.Amount);
            result.Value.Spending.Currency.ShouldBe(CorrectSpending.Currency);
            result.Value.TransactionDate.Value.ShouldBe(CorrectTransactionDate.Value);
            result.Value.Type.Value.ShouldBe(CorrectType.Value);
        }

        [Test]
        public void GivenCorrectParameters_WhenExpenseIsCreated_SuccessResultReturnedAndToStringsReturnCorrectValues()
        {
            // Act 
            var result = Expense.Create(CorrectId, CorrectRecipient, CorrectSpending, CorrectTransactionDate, CorrectType);

            // Assert
            result.IsSuccess.ShouldBeTrue();

            result.Value.Id.ToString().ShouldBe(CorrectId.Value.ToString());
            result.Value.Recipient.ToString().ShouldBe(CorrectRecipient.Name);
            result.Value.Spending.ToString().ShouldBe($"{CorrectSpending.Currency} {CorrectSpending.Amount}");
            result.Value.TransactionDate.ToString().ShouldBe(CorrectTransactionDate.Value.ToString("dd/MM/yyyy"));
            result.Value.Type.ToString().ShouldBe(CorrectType.Value);
        }

        [Test]
        public void GivenExpenseId_WhenCastToInt_CorrectValueReturned()
        {
            // Act
            var result = (int)CorrectId;

            // Assert
            result.ShouldBe(CorrectId.Value);
        }

        [Test]
        public void GivenTransactionDate_WhenCastToDateTime_CorrectValueReturned()
        {
            // Act
            var result = (DateTime)CorrectTransactionDate;

            // Assert
            result.ShouldBe(CorrectTransactionDate.Value);
        }

        [Test]
        public void GivenTwoIdenticalMoneyInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctSpendingCopy = Money.Create(CorrectSpending.Amount, CorrectSpending.Currency).Value;

            // Act
            var result = CorrectSpending == correctSpendingCopy;

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void GivenTwoIdenticalExpenseTypeInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctTypeCopy = ExpenseType.Create(CorrectType.Value).Value;

            // Act
            var result = CorrectType == correctTypeCopy;

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void GivenTwoIdenticalTransactionDateInstances_WhenCompared_CorrectValueReturned()
        {
            // Arrange
            var correctTransactionDateCopy = TransactionDate.Create(CorrectTransactionDate.Value).Value;

            // Act
            var result = CorrectTransactionDate == correctTransactionDateCopy;

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void GivenTwoDifferentMoneyInstancesWhichDifferByAmount_WhenCompared_CorrectValueReturned()
        {
            // Act
            var result = CorrectSpending == CorrectSpendingDifferingByAmount;

            // Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void GivenTwoDifferentMoneyInstancesWhichDifferByCurrency_WhenCompared_CorrectValueReturned2()
        {
            // Act
            var result = CorrectSpending == CorrectSpendingDifferingByCurrency;

            // Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void GivenTwoDifferentExpenseTypeInstances_WhenCompared_CorrectValueReturned()
        {
            // Act
            var result = CorrectType == AnotherCorrectType;

            // Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void GivenTwoDifferentTransactionDateInstances_WhenCompared_CorrectValueReturned()
        {
            // Act
            var result = CorrectTransactionDate == AnotherCorrectTransactionDate;

            // Assert
            result.ShouldBeFalse();
        }
    }
}