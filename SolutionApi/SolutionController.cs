using Microsoft.AspNetCore.Mvc;

namespace SolutionApi;

[ApiController]
public class SolutionController : ControllerBase
{
    public record CallCombinationSumCommand(int[] Candidates, int Target);
    
    [HttpPost("combination-sum")]
    public IList<IList<int>> Post(CallCombinationSumCommand command)
    {
        return new Solution().CombinationSum(command.Candidates, command.Target);
    }
}