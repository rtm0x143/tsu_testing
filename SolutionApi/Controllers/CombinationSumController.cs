using Microsoft.AspNetCore.Mvc;

namespace SolutionApi.Controllers;

public record CallCombinationSumCommand(int[] Candidates, int Target);

[ApiController]
[Route("[controller]")]
public class CombinationSumController : ControllerBase
{
    [HttpPost]
    public ActionResult<IList<IList<int>>> Post(CallCombinationSumCommand command)
    {
        try
        {
            return Ok(new Solution().CombinationSum(command.Candidates, command.Target));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}