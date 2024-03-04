using AutoFixture;
using ExpenseTracker.Domain.Abstractions;
using FluentAssertions;

namespace ExpenseTracker.Domain.Tests.Abstractions
{
    public sealed class ErrorTests
    {
        private readonly Fixture _fixture = new();

        [Test]
        public void GivenErrorsInstance_WhenCombinedWithAnotherErrorsInstance_CorrectResultReturned()
        {
            // Arrange
            var errorCodeOne = _fixture.Create<string>();
            var errorCodeTwo = _fixture.Create<string>();

            var errorsOne = new Errors([new Error(errorCodeOne)]);
            var errorsTwo = new Errors([new Error(errorCodeTwo)]);

            // Act
            var result = errorsOne.Combine(errorsTwo);

            // Assert
            result.Should().BeOfType<Errors>();
            ((Errors)result).Count.Should().Be(2);
            ((Errors)result).Select(error => error.Code).Should().BeEquivalentTo(new List<string> {errorCodeOne, errorCodeTwo});
        }
    }
}
