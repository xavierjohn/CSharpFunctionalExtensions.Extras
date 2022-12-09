namespace CSharpFunctionalExtensions.Errors.Tests;
using FluentAssertions;

public class ErrorListTests
{
    [Fact]
    public void Combine_ErrorList()
    {
        // Arrange
        var emailResultSuccess = EmailAddress.Create("xavier@somewhere.com");
        var stringResultSuccess = Result.Success<string, ErrorList>("one");
        var emailResultFailure = EmailAddress.Create("Bad Email");
        var stringResultFailure = Result.Failure<string, ErrorList>(Error.Validation("FirstName", "firstName is required"));

        // Act
        var result = Result.Combine<ErrorList>(emailResultSuccess, stringResultSuccess, emailResultFailure, stringResultFailure);

        // Assert
        var expected = new ErrorList(
            Error.Validation("FirstName", "firstName is required"),
            Error.Validation("emailString", "Bad email address"));

        result.IsFailure.Should().BeTrue();
        result.Error.HasErrors.Should().BeTrue();
        result.Error.Should().HaveCount(2);
        result.Error.Should().BeEquivalentTo(expected);
    }
}
