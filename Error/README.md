# CSharpFunctionalExtensions.Errors

This library has the following errors classes

#### ErrorList
`ErrorList` To help return a collection of errors.

It can be used to combine multiple errors using the `Combine` method.

**Example:**
```csharp
    var stringResultSuccess = Result.Success<string, ErrorList>("one");
    var emailResultFailure = EmailAddress.Create("Bad Email");
    var result = Result.Combine<ErrorList>(stringResultSuccess, emailResultFailure);
```

#### Common Error classes
The following error classes are available.

- Conflict
- NotFound
- Unauthorized
- Unexpected
- Validation


#### Fluent Validation Extension
The following extension will convert Fluent Validation errors to `ErrorList` with `Error.Validation errors`.

```csharp
public static Result<T, ErrorList> ToResult<T>(this FluentValidation.Results.ValidationResult validationResult, T value)
   
```

**Example:**
Look at `s_validator.Validate(user).ToResult(user);` below.
```csharp
public class User : Entity<Guid>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }

    public static Result<User, ErrorList> Create(string firstName, string lastName, string email, string password)
    {
        var user = new User(firstName, lastName, email, password);
        return s_validator.Validate(user).ToResult(user);
    }


    private User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    static readonly InlineValidator<User> s_validator = new()
    {
        v => v.RuleFor(x => x.FirstName).NotEmpty(),
        v => v.RuleFor(x => x.LastName).NotEmpty(),
        v => v.RuleFor(x => x.Email).NotEmpty().EmailAddress(),
        v => v.RuleFor(x => x.Password).NotEmpty()
    };
}
```
