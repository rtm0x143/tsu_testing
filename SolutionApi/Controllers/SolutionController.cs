using Microsoft.AspNetCore.Mvc;
using SolutionApi.ViewModels;

namespace SolutionApi.Controllers;

public class SolutionController : Controller
{
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Execute([FromForm] SolutionViewModel model)
    {
        if (!ModelState.IsValid) return View("Index");

        var candidates = new List<int>();
        foreach (var candidate in model.CandidatesSeq.Split(',', ';').Select(s => s.Trim()))
        {
            if (!int.TryParse(candidate, out var num))
            {
                ModelState.AddModelError(nameof(model.CandidatesSeq),
                    "Should be string of integers, separated by ',' or '';");
                return View("Index");
            }

            candidates.Add(num);
        }

        using var httpClient = new HttpClient();
        var message = await httpClient.PostAsJsonAsync($"{Request.Scheme}://{Request.Host}/CombinationSum",
            new CallCombinationSumCommand(candidates.ToArray(), model.Target));

        if (!message.IsSuccessStatusCode)
        {
            ModelState.AddModelError(nameof(model.Result), await message.Content.ReadAsStringAsync());
            return View("Index");
        }

        var result = await message.Content.ReadFromJsonAsync<IList<IList<int>>>();

        model.Result = result.Count > 0
            ? string.Join(",\n", result!.Select(row => $"[ {string.Join(", ", row)} ]"))
            : "[ ]";

        return View("Index", model);
    }
}