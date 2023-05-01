namespace SolutionApi;

public class Solution
{
    private readonly IList<IList<int>> _result = new List<IList<int>>();

    private void _backtrack(int index, List<int> path, int total, int[] candidates, int target)
    {
        if (total == target)
        {
            _result.Add(path.ToList());
            return;
        }

        if (total > target || index >= candidates.Length) return;

        path.Add(candidates[index]);
        _backtrack(index,
            path,
            total + candidates[index],
            candidates,
            target);

        path.Remove(path.Last());

        _backtrack(index + 1,
            path,
            total,
            candidates,
            target);
    }

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        if (candidates.Length is < 1 or > 30)
            throw new ArgumentException("Collection had invalid length", nameof(candidates));
        if (candidates.Distinct().Count() != candidates.Length)
            throw new ArgumentException("Collection contained not distinct items", nameof(candidates));
        if (candidates.Any(item => item is < 2 or > 40))
            throw new ArgumentOutOfRangeException(nameof(candidates), "Some collection's item is invalid");
        if (target is < 1 or > 40)
            throw new ArgumentOutOfRangeException(nameof(target));

        _backtrack(0, new List<int>(), 0, candidates, target);
        return _result;
    }
}