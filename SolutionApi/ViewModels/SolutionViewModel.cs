using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace SolutionApi.ViewModels;

public class SolutionViewModel : IValidatableObject
{
    public string CandidatesSeq { get; set; }
    [Range(1, 40)] public int Target { get; set; }
    [HiddenInput] public string? Result { get; set; }

    public bool TryParseCandidatesSeq([NotNullWhen(true)] out int[]? candidates)
    {
        var numberStrings = CandidatesSeq.Split(',', ';')
            .Select(s => s.Trim())
            .ToArray();
        candidates = new int[numberStrings.Length];
        for (var i = 0; i < numberStrings.Length; i++)
        {
            if (!int.TryParse(numberStrings[i], out var num) || num is < 1 or > 40)
            {
                candidates = null;
                return false;
            }

            candidates[i] = num;
        }

        return true;
    }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TryParseCandidatesSeq(out var _)) yield return ValidationResult.Success!;
        else
        {
            yield return new ValidationResult("Should be string of integers in range [1; 40], separated by ',' or '';",
                new[] { nameof(CandidatesSeq) });
        }
    }
}