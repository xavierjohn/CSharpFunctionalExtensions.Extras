namespace SampleAPI.Controllers.Models;

public record RegisterRequest(
    string firstName,
    string lastName,
    string email,
    string password
);
