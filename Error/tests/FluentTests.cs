namespace CSharpFunctionalExtensions.Errors.Tests
{
    using FluentAssertions;
    using Xunit;

    public class FluentTests
    {
        [Fact]
        public void Will_return_failure_on_validation_error()
        {
            // Arrange
            var expected = new ErrorList(Error.Validation("emailString", "Bad email address"));

            // Act
            var result = EmailAddress.Create("Bad Email");


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Will_return_object_on_success()
        {
            // Arrange

            // Act
            var result = EmailAddress.Create("xavier@somewhere.com");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<EmailAddress>();
            result.Value.Value.Should().Be("xavier@somewhere.com");
        }
    }
}
