# CSharpFunctionalExtensions.Asp

ToActionResult extension method converts Result<T, Error> from CsharpFunctionalExtensions to Asp.Net Core ActionResult.

Run the sample program to try it out.

Example:

```csharp
   public ActionResult<AuthenticationResult> Register(RegisterRequest request) =>
        Domain.User.Create(request.firstName, request.lastName, request.email, request.password)
             .Bind(user => Result.Success<AuthenticationResult, ErrorList>(new AuthenticationResult(user, "token")))
             .ToActionResult(this);
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