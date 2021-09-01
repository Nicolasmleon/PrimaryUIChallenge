using OpenQA.Selenium;

namespace PrimaryChallenge.PageObjects
{
    class PublicationPage
    {
        // Paths
        #region
        private readonly By _publicationTitle = By.XPath("//div[contains(@class,'ui-pdp')]//h1");
        private readonly By _publicationMainPrice = By.XPath("//div[contains(@class,'ui-pdp-price--size-large')]//div[contains(@class,'ui-pdp-price__second-line')]//span[contains(@class,'price-tag ui-pdp-price__part')]//span[contains(@class,'price-tag-text-sr-only')]");
        #endregion

        public IWebDriver WebDriver { get; }
        public PublicationPage(IWebDriver driver) => WebDriver = driver;

        // UI Elements
        #region
        public IWebElement txtPublicationTitle => WebDriver.FindElement(_publicationTitle);
        public IWebElement txtPublicationMainPrice => WebDriver.FindElement(_publicationMainPrice);
        #endregion

        public string getActualTitle()
        {
            return txtPublicationTitle.Text;
        }

        public string getActualPrice()
        {
            return txtPublicationMainPrice.Text;
        }
    }
}
