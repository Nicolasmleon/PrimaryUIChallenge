using OpenQA.Selenium;
using System.Threading;

namespace PrimaryChallenge.PageObjects
{
    public class CategoriesPage
    {
        // Paths
        #region
        private readonly By _searchInCategory = By.XPath("//div[contains(@class,'nav-menu')]//a[contains(@class,'nav-menu-categories-link') and contains (text(), 'Categorías')]");
        private readonly By _searchInTechnology = By.XPath("//a[contains(text(),'Tecnología')]");
        private By _searchInProducts, _selectBrand, _searchForType;
        #endregion

        public IWebDriver WebDriver { get; }
        public CategoriesPage(IWebDriver driver) => WebDriver = driver;

        // UI Elements
        #region
        public IWebElement btnCategories => WebDriver.FindElement(_searchInCategory);
        public IWebElement btnTechnology => WebDriver.FindElement(_searchInTechnology);
        public IWebElement btnProducts => WebDriver.FindElement(_searchInProducts);
        public IWebElement sltBrand => WebDriver.FindElement(_selectBrand);
        public IWebElement srcForType => WebDriver.FindElement(_searchForType);
        #endregion

        public void Search(string product, string category)
        {
            _searchInProducts = By.XPath("//a[contains(text(),'"+ category +"')]");

            _selectBrand = By.XPath("//div[contains(@class,'label')]/h3[contains(text(),'"+ product +"')]");

            btnProducts.Click();
            Thread.Sleep(500);
            sltBrand.Click();
        }

        public void TechCategory(string product)
        {
            if (product == "SAMSUNG")
            {
                _searchForType = By.XPath("//a[contains(text(),'Celulares y Teléfonos')]");
            }
            else if (product == "Televisores")
            {
                _searchForType = By.XPath("//a[contains(text(),'Televisores')]");
            }

            srcForType.Click();
        }

        public void SearchInTechnology(string product)
        {
            _selectBrand = By.XPath("//div[contains(@class,'label')]/h3[contains(text(),'" + product + "')]");

            Thread.Sleep(2000);

            sltBrand.Click();
        }
    }
}
