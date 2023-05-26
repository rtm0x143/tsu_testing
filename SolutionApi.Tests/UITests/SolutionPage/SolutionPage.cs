using System.Runtime.InteropServices.JavaScript;
using OpenQA.Selenium;

namespace SolutionApi.Tests.UITests.SolutionPage;

public class SolutionPage
{
    private readonly ISearchContext _searchContext;

    public SolutionPage(ISearchContext searchContext) => _searchContext = searchContext;

    public IWebElement CandidatesSeq => _searchContext.FindRequiredElement(By.Id("CandidatesSeq"));
    public IWebElement Target => _searchContext.FindRequiredElement(By.Id("Target"));
    public IWebElement Execute => _searchContext.FindRequiredElement(By.Id("Execute"));
    public IWebElement Result => _searchContext.FindRequiredElement(By.XPath("/html/body/div/form/div[6]"));

    public IEnumerable<IWebElement> ValidationErrors =>
        _searchContext.FindElements(By.ClassName("field-validation-error"));

    public IWebElement ResultError
    {
        get
        {
            var element = ValidationErrors.FirstOrDefault(
                element => element.GetDomAttribute("data-valmsg-for") == "Result");
            Assert.IsNotNull(element);
            return element!;
        }
    }

    public IWebElement TargetError
    {
        get
        {
            var element = ValidationErrors.FirstOrDefault(
                element => element.GetDomAttribute("data-valmsg-for") == "Target");
            Assert.IsNotNull(element);
            return element!;
        }
    }

    public IWebElement CandidatesSeqError
    {
        get
        {
            var element = ValidationErrors.FirstOrDefault(
                element => element.GetDomAttribute("data-valmsg-for") == "CandidatesSeq");
            Assert.IsNotNull(element);
            return element!;
        }
    }
}