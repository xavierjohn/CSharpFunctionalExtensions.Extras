namespace CSharpFunctionalExtensions.Errors;

public sealed class Conflict : Error
{
    public Conflict(string code, string message) : base(code, message)
    {
    }

}
