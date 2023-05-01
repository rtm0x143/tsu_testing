namespace SolutionApi.Tests.UnitTests;

[TestFixture]
public class SolutionTests
{
    private static void _assertCombinationsEquivalent(IEnumerable<IEnumerable<int>> expected,
        IEnumerable<IEnumerable<int>> combinations)
    {
        var combinationsDictionaries = combinations.Select(
            comb => comb.GroupBy(num => num)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()));

        var expectedDictionaries = expected.Select(
            comb => comb.GroupBy(num => num)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()));

        foreach (var combinationsDict in combinationsDictionaries)
        {
            Assert.That(expectedDictionaries.Any(
                    expectedDict => combinationsDict.All(
                        pair => expectedDict.ContainsKey(pair.Key) && expectedDict[pair.Key] == pair.Value)),
                Is.True,
                "Some expected combination wasn't found");
        }
    }

    public record struct CombinationSumTestCaseSource(int[] Candidates, int Target, int[][]? ExpectedResult = null);

    private static CombinationSumTestCaseSource[] _validCombinationSumTestCaseSource =
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

    [Test(Description = "Checks behaviour if valid arguments are provided")]
    [TestCaseSource(nameof(_validCombinationSumTestCaseSource))]
    public void GivenValidParameters_WhenCombinationSumCalled_ThenReturnCorrectResult(CombinationSumTestCaseSource args)
    {
        var sut = new Solution();
        IList<IList<int>> combinations = null!;

        Assert.DoesNotThrow(() =>
            combinations = sut.CombinationSum(args.Candidates, args.Target));

        Assert.That(combinations, Has.Count.EqualTo(args.ExpectedResult!.Length));
        _assertCombinationsEquivalent(args.ExpectedResult, combinations);
    }

    private static CombinationSumTestCaseSource[] _invalidCandidatesLengthCombinationSumTestCaseSource =
    {
        new(Candidates: Enumerable.Range(1, 31).ToArray(), Target: 6),
        new(Candidates: Array.Empty<int>(), Target: 6)
    };

    [Test(Description = $"Test with invalid {nameof(args.Candidates)} collection length")]
    [TestCaseSource(nameof(_invalidCandidatesLengthCombinationSumTestCaseSource))]
    public void GivenInvalidCandidatesLength_WhenCombinationSumCalled_ThenThrowsArgumentException(
        CombinationSumTestCaseSource args)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentException>(() => sut.CombinationSum(args.Candidates, args.Target));
    }

    [TestCase(new[] { 1 }, 1, Description = "Case with candidates item value less than the lower bound")]
    [TestCase(new[] { 41 }, 1, Description = "Case with candidates item value above the upper bound")]
    public void GivenInvalidCandidatesValues_WhenCombinationSumCalled_ThenThrowsArgumentOutOfRangeException(
        int[] candidates, int target)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.CombinationSum(candidates, target));
    }

    [TestCase(new[] { 3, 3 }, 6, Description = "Case with not distinct candidates values")]
    public void GivenNotDistinctCandidatesValues_WhenCombinationSumCalled_ThenThrowsArgumentException(
        int[] candidates, int target)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentException>(() => sut.CombinationSum(candidates, target));
    }

    [TestCase(new[] { 3 }, 0, Description = "Case with target's value less than the lower bound")]
    [TestCase(new[] { 3 }, 41, Description = "Case with target's value above the upper bound")]
    public void GivenInvalidTargetValue_WhenCombinationSumCalled_ThenThrowsArgumentOutOfRangeException(
        int[] candidates, int target)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.CombinationSum(candidates, target));
    }
}