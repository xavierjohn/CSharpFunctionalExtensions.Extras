﻿namespace CSharpFunctionalExtensions.Errors;

using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Diagnostics;

[DebuggerDisplay("{Message}")]
public class Error : ValueObject
{
    public string Code { get; }
    public string Message { get; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public static Error Conflict(string code, string message) =>
        new Conflict(code, message);

    public static Error NotFound(string code, string message) =>
        new NotFound(code, message);

    public static Error Validation(string code, string message, string fieldname) =>
       new Validation(code, message, fieldname);

    public static Error Unauthorized(string code, string message) =>
        new Unauthorized(code, message);

}

