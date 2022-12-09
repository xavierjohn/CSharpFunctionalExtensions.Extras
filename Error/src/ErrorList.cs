namespace CSharpFunctionalExtensions.Errors;

using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions.ValueTasks;

public sealed class ErrorList : List<Error>, ICombine
{
    public ErrorList() { }
    public ErrorList(IEnumerable<Error> errors) : base(errors) { }
    public ErrorList(params Error[] errors) : base(errors) { }

    public bool HasErrors => Count > 0;

    public void Add(ErrorList ec) => AddRange(ec);


    ICombine ICombine.Combine(ICombine value)
    {
        var errorList = new ErrorList((ErrorList)value);
        errorList.AddRange(this);
        return new ErrorList(errorList);
    }

    public static implicit operator ErrorList(Error e) => new(new List<Error>() { e });

    public static Result<bool, ErrorList> Combine(params Result<object, ErrorList>[] results)
    {
        var failedResults = results.Where(x => x.IsFailure).ToList();
        if (failedResults.Count == 0)
            return Result.Success<bool, ErrorList>(true);

        ErrorList errorList = new();
        failedResults.ForEach(e => errorList.Add(e.Error));
        return Result.Failure<bool, ErrorList>(errorList);
    }

}
