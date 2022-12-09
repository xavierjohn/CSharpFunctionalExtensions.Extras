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

# CSharpFunctionalExtensions.Asp

ASP base controller for use with CSharpFunctionalExtensions

This library converts CSharpFunctionalExtensions.Result to HTTP errors but using the base controller
`CSharpFunctionalBase` and calling the method `MapToOkObjectResult`

Run the sample program to try it out.

Example:

```csharp
   public ActionResult<AuthenticationResult> Register(RegisterRequest request) =>
        Domain.User.Create(request.firstName, request.lastName, request.email, request.password)
             .Bind(user => Result.Success<AuthenticationResult, ErrorList>(new AuthenticationResult(user, "token")))
             .Finally(authResult => MapToActionResult(authResult));
```

If a validation failure occurs, it will return a response like

```
HTTP/1.1 400 Bad Request
Connection: close
Content-Type: application/problem+json; charset=utf-8
Date: Mon, 14 Nov 2022 20:29:19 GMT
Server: Kestrel
Transfer-Encoding: chunked
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-19b910c84bc8651a1c6d26e9ee640813-59f0f7943ce1d3ab-00",
  "errors": {
    "lastName": [
      "The lastName field is required."
    ],
    "firstName": [
      "The firstName field is required."
    ]
  }
}
```