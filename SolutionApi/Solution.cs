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
        _backtrack(0, new List<int>(), 0, candidates, target);
        return _result;
    }
}