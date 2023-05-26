using OpenQA.Selenium;

namespace SolutionApi.Tests.UITests;

public static class SearchContextExtensions
{
    public static IWebElement FindRequiredElement(this ISearchContext searchContext, By locator)
    {
        var element = searchContext.FindElement(locator);
        Assert.IsNotNull(element);
        return element;
    }
}