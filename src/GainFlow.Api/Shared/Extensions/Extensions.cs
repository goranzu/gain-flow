using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace GainFlow.Api.Shared.Extensions;

public static class Extensions
{
    public static Dictionary<string, string[]> ToProblemDetailErrors(
        this List<ValidationFailure> validationFailures)
    {
        var errors = validationFailures.GroupBy(g => g.PropertyName)
            .ToDictionary(g => g.Key.ToLowerInvariant(), g => g.Select(e => e.ErrorMessage).ToArray());
        return errors;
    }

    public static Dictionary<string, object?> ToErrorsDictionary(this IdentityResult identityResult)
    {
        return new Dictionary<string, object?>
        {
            { "errors", identityResult.Errors.ToDictionary(e => e.Code.ToLowerInvariant(), e => e.Description) }
        };
    }
}
