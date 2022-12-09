namespace SampleAPI.Controllers.Models;

public record AuthenticationResult
(
    Domain.User user,
    string token
);
