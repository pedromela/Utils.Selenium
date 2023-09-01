using OpenQA.Selenium;

namespace Utils.Selenium
{
    public static class DriverExtensions
    {
        public static IWebElement GetParent(this IWebElement node)
        {
            return node.FindElement(By.XPath(".."));
        }
    }
}
