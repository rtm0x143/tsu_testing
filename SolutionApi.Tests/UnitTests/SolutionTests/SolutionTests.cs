using SolutionApi.Tests.TestCaseSources;

namespace SolutionApi.Tests.UnitTests;

[TestFixture]
public class SolutionTests
{
    [Test(Description = "Checks behaviour if valid arguments are provided")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.ValidTestCaseSource))]
    public void GivenValidParameters_WhenCombinationSumCalled_ThenReturnCorrectResult(
        CombinationSumSources.SourceType args)
    {
        var sut = new Solution();
        IList<IList<int>> combinations = null!;

        Assert.DoesNotThrow(() =>
            combinations = sut.CombinationSum(args.Candidates, args.Target));

        Assert.That(combinations, Has.Count.EqualTo(args.ExpectedResult!.Length));
        AssertHelper.AssertSumCombinationsEquivalent(args.ExpectedResult, combinations);
    }

    [Test(Description = $"Test with invalid {nameof(args.Candidates)} collection length")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidCandidatesLengthTestCaseSource))]
    public void GivenInvalidCandidatesLength_WhenCombinationSumCalled_ThenThrowsArgumentException(
        CombinationSumSources.SourceType args)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentException>(() => sut.CombinationSum(args.Candidates, args.Target));
    }

    [Test(Description = $"Test with invalid {nameof(args.Candidates)} values")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidCandidatesValuesTestCaseSource))]
    public void GivenInvalidCandidatesValues_WhenCombinationSumCalled_ThenThrowsArgumentOutOfRangeException(
        CombinationSumSources.SourceType args)
    {
        var sut = new Solution();
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.CombinationSum(args.Candidates, args.Target));
    }

    [Test(Description = "Cases with not distinct candidates values")]
    [TestCaseSource(typeof(CombinationSumSources),
        nameof(CombinationSumSources.NotDistinctCandidatesValuesTestCaseSource))]
    public void GivenNotDistinctCandidatesValues_WhenCombinationSumCalled_ThenThrowsArgumentException(
        CombinationSumSources.SourceType args)
    {
        var sut = new Solution();

        Assert.Throws<ArgumentException>(() => sut.CombinationSum(args.Candidates, args.Target));
    }

    [Test(Description = $"Test with invalid {nameof(args.Target)} values")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidTargetValueTestCaseSource))]
    public void GivenInvalidTargetValue_WhenCombinationSumCalled_ThenThrowsArgumentOutOfRangeException(
        CombinationSumSources.SourceType args)
    {
        var sut = new Solution();
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.CombinationSum(args.Candidates, args.Target));
    }

    [Test(Description = "Check behaviour when algorithm called multiple times on the same instance")]
    public void GivenValidParameters_WhenCombinationSumCalledMultipleTimes_ThenResultsAreSame()
    {
        int[] candidates = { 3, 4, 5, 6, 7, 8 };
        const int target = 30;
        var sut = new Solution();

        var result1 = sut.CombinationSum(candidates, target);
        var result2 = sut.CombinationSum(candidates, target);

        AssertHelper.AssertSumCombinationsEquivalent(result1, result2);
    }
}