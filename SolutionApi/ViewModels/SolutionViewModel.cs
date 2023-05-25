using Microsoft.AspNetCore.Mvc;

namespace SolutionApi.ViewModels;

public class SolutionViewModel
{
    public string CandidatesSeq { get; set; }
    public int Target { get; set; }
    [HiddenInput] public string? Result { get; set; }
}