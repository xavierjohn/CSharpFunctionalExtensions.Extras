﻿namespace CSharpFunctionalExtensions.Errors;

public sealed class NotFound : Error
{
    public NotFound(string code, string message) : base(code, message)
    {
    }
}
