namespace SolutionApi.Tests.TestCaseSources;

public class CombinationSumSources
{
    public record struct Source(int[] Candidates, int Target, int[][]? ExpectedResult = null);

    public static readonly Source[] ValidTestCaseSource =
    {
        /* Simple cases */
        new(Candidates: new[] { 2, 3, 6, 7 }, Target: 7, ExpectedResult: new[]
        {
            new[] { 2, 2, 3 },
            new[] { 7 }
        }),
        new(Candidates: new[] { 2, 3, 5 }, Target: 8, ExpectedResult: new[]
        {
            new[] { 2, 2, 2, 2 },
            new[] { 2, 3, 3 },
            new[] { 3, 5 }
        }),
        new(Candidates: new[] { 2, }, Target: 1, ExpectedResult: Array.Empty<int[]>()),
        /* Bound values cases */
        // candidates lower bound length 
        new(Candidates: new[] { 3 }, Target: 6, ExpectedResult: new[] { new[] { 3, 3 } }),
        // candidates upper bound length
        new(Candidates: Enumerable.Range(3, 30).ToArray(), Target: 6, ExpectedResult: new[]
        {
            new[] { 6 },
            new[] { 3, 3 }
        }),
        // candidates item lower bound value
        new(Candidates: new[] { 2, 3 }, Target: 6, ExpectedResult: new[]
        {
            new[] { 2, 2, 2 },
            new[] { 3, 3 }
        }),
        // candidates item upper bound value
        new(Candidates: new[] { 30, 3 }, Target: 30, ExpectedResult: new[]
        {
            Enumerable.Repeat(3, 10).ToArray(),
            new[] { 30 }
        }),
        // target lower bound value
        new(Candidates: new[] { 3, 4 }, Target: 1, ExpectedResult: Array.Empty<int[]>()),
        // target upper bound value
        new(Candidates: new[] { 5, 7 }, Target: 40, ExpectedResult: new[]
        {
            Enumerable.Repeat(7, 5).Append(5).ToArray(),
            Enumerable.Repeat(5, 8).ToArray()
        })
    };

    public static readonly Source[] InvalidCandidatesLengthTestCaseSource =
    {
        new(Candidates: Enumerable.Range(1, 31).ToArray(), Target: 6),
        new(Candidates: Array.Empty<int>(), Target: 6)
    };

    public static readonly Source[] InvalidCandidatesValuesTestCaseSource =
    {
        new(Candidates: new[] { 1 }, Target: 1), // less than the lower bound
        new(Candidates: new[] { 41 }, Target: 1) // above the upper bound
    };

    public static readonly Source[] InvalidTargetValueTestCaseSource =
    {
        new(Candidates: new[] { 3 }, Target: 0), // less than the lower bound
        new(Candidates: new[] { 3 }, Target: 41) // above the upper bound
    };

    public static readonly Source[] NotDistinctCandidatesValuesTestCaseSource =
    {
        new(Candidates: new[] { 3, 3 }, Target: 6)
    };
}