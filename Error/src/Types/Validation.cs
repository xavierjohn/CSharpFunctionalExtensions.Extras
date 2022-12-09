﻿namespace CSharpFunctionalExtensions.Errors;

public sealed class Validation : Error
{
    public Validation(string code, string message) : base(code, message)
    {
    }
}
