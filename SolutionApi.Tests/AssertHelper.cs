namespace SolutionApi.Tests;

public static class AssertHelper
{
    public static void AssertSumCombinationsEquivalent(IEnumerable<IEnumerable<int>>? expected,
        IEnumerable<IEnumerable<int>>? combinations)
    {
        Assert.NotNull(combinations, "combinations != null");
        Assert.NotNull(expected, "expected != null");

        var combinationsDictionaries = combinations!.Select(
            comb => comb.GroupBy(num => num)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()));

        var expectedDictionaries = expected!.Select(
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
}