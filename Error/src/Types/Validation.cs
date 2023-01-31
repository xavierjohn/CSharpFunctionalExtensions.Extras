namespace CSharpFunctionalExtensions.Errors;

public sealed class Validation : Error
{
    public string FieldName { get; }

    public Validation(string code, string message, string fieldName) : base(code, message)
    {
        FieldName = fieldName;
    }
}
