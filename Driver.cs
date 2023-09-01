using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Utils.Utils;

namespace Utils.Selenium
{
    public enum DelayType
    {
        Micro,
        Normal,
        Long,
        Super
    }
    public class Driver
    {
        private readonly int TIMEOUT = 15;
        public IWebDriver browser;
        public Driver()
        {
            //Init();
        }

        protected virtual void Init()
        {
            var service = ChromeDriverService.CreateDefaultService("./chromedriver_win32_102");
            service.LogPath = "./chromedriver.log";
            service.EnableVerboseLogging = true;
            browser = new ChromeDriver(service, PrepateOptions());
        }

        protected void Quit()
        {
            browser.Quit();
        }


        private ChromeOptions PrepateOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            options.BinaryLocation = "./Chrome/Application/chrome.exe";
            return options;
        }

        public void Delay(DelayType type = DelayType.Normal)
        {
            switch (type)
            {
                case DelayType.Micro:
                    Thread.Sleep(RandomGenerator.RandomNumber(0, 1000));
                    break;
                case DelayType.Normal:
                    Thread.Sleep(RandomGenerator.RandomNumber(1000, 3000));
                    break;
                case DelayType.Long:
                    Thread.Sleep(RandomGenerator.RandomNumber(3000, 10000));
                    break;
                case DelayType.Super:
                    Thread.Sleep(RandomGenerator.RandomNumber(15 * 60 * 1000, 45 * 60 * 1000));
                    break;
                default:
                    Thread.Sleep(RandomGenerator.RandomNumber(3000, 10000));
                    break;
            }
        }

        public string GetCurrentSource()
        {
            return browser.PageSource;
        }

        public void Get(string url)
        {
            browser.Url = url;
            browser.Navigate();
            Delay();
        }

        public void SendKeysToInputById(string id, string value)
        {
            try
            {
                IWebElement element = WaitForElementById(id);
                Delay();
                element.SendKeys(value);
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }

        public void SendKeysToInputByName(string name, string value)
        {
            try
            {
                IWebElement element = WaitForElementByName(name);
                Delay();
                element.SendKeys(value);
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }

        public void SendKeysToInputByXPath(string xpath, string value)
        {
            try
            {
                IWebElement element = WaitForElementByXPath(xpath);
                Delay();
                element.SendKeys(value);
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }

        public void ClickButtonById(string id)
        {
            try
            {
                IWebElement element = WaitForElementById(id);
                Delay();
                element.Click();
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }
        public void ClickButtonByName(string name)
        {
            try
            {
                IWebElement element = WaitForElementByName(name);
                Delay();
                element.Click();
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }

        public void ClickButtonByXPath(string xpath)
        {
            try
            {
                IWebElement element = WaitForElementByXPath(xpath);
                if (element == null)
                {
                    Console.WriteLine("Driver::ClickButtonByXPath(string xpath) -> element is null.");
                    return;
                }
                Delay();
                element.Click();
            }
            catch (Exception e)
            {
                UtilsLib.UtilsLib.DebugMessage(e);
            }
        }

        public IWebElement WaitForElementById(string id)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(TIMEOUT));
                return wait.Until(driver => driver.FindElement(By.Id(id)));
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
            catch (TimeoutException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IWebElement WaitForElementByName(string name)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(TIMEOUT));
                return wait.Until(driver => driver.FindElement(By.Name(name)));
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
            catch (TimeoutException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IWebElement WaitForElementByXPath(string xpath, int timeout = 0)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(timeout <= 0 ? TIMEOUT : timeout));
                return wait.Until(driver => FindElement(driver, xpath));
            }
            catch (TimeoutException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int CountElements(string xpath)
        {
            return FindElements(browser, xpath).Count;
        }

        private IWebElement FindElement(IWebDriver driver, string xpath)
        {
            try
            {
                return driver.FindElement(By.XPath(xpath));
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
        }

        private ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver, string xpath)
        {
            try
            {
                return driver.FindElements(By.XPath(xpath));
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
        }

        protected void Click(IWebElement element)
        {
            try
            {
                if (element == null)
                {
                    Console.WriteLine("Driver::Click(IWebElement element) -> element is null.");
                    return;
                }
                element.Click();
                Delay(DelayType.Micro);
            }
            catch (Exception)
            {
            }
        }
    }
}
