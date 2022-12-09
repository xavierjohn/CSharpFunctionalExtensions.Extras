namespace SampleAPI.Controllers;

using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.Asp;
using CSharpFunctionalExtensions.Errors;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Controllers.Models;

[ApiController]
[Route("auth")]
public class AuthenticationController : CSharpFunctionalBase
{
    [HttpPost("register")]
    public ActionResult<AuthenticationResult> Register(RegisterRequest request) =>
        Domain.User.Create(request.firstName, request.lastName, request.email, request.password)
             .Bind(user => Result.Success<AuthenticationResult, ErrorList>(new AuthenticationResult(user, "token")))
             .Finally(authResult => MapToActionResult(authResult));
}
