using OpenQA.Selenium;
using System;

namespace PrimaryChallenge.PageObjects
{
    public class ResultsPage
    {
        // Paths
        #region
        private readonly By _resultsAmount = By.XPath("//div[contains(@class,'ui-search-search-result')]//span");
        private By _searchTitle, _zoneFilter, _publicationTitles, _publicationPrices;
        #endregion

        int publications;

        public IWebDriver WebDriver { get; }
        public ResultsPage(IWebDriver driver) => WebDriver = driver;

        // UI Elements
        #region
        public IWebElement txtResult => WebDriver.FindElement(_searchTitle);
        public IWebElement txtResultsAmount => WebDriver.FindElement(_resultsAmount);
        public IWebElement txtFilterZoneName => WebDriver.FindElement(_zoneFilter);
        public IWebElement txtPublicationsTitle => WebDriver.FindElement(_publicationTitles);
        public IWebElement txtPublicationsPrice => WebDriver.FindElement(_publicationPrices);
        #endregion

        public bool IsListDisplayed(string product)
        {
            _searchTitle = By.XPath("//div[contains(@class,'ui-search-breadcrumb')]//h1[contains(text(),'" + product + "')]");

            return txtResult.Displayed;
        }

        public string getNumberOfResults()
        {
            string result = txtResultsAmount.Text;

            return result;
        }

        public void applyFilterByZone(string zone)
        {
            _zoneFilter = By.XPath("//a[contains(@aria-label,'" + zone + "')]/span[contains(@class,'ui-search-filter-name')]");

            txtFilterZoneName.Click();
        }

        public int getNumberOfPublications()
        {
            _publicationTitles = By.XPath("//li//div[contains(@class,'ui-search-item__group ui-search-item__group--title')]/a/h2");
            Console.WriteLine("HAY UN TOTAL DE: " + publications + " PUBLICACIONES");

            return publications = txtPublicationsTitle.FindElements(_publicationTitles).Count;
        }

        public string setTitlePublication(string number)
        {
            _publicationTitles = By.XPath("//li[" + number + "]//div[contains(@class,'ui-search-item__group ui-search-item__group--title')]/a/h2");

            Console.WriteLine("LA PUBLICACION ELEGIDA ES: " + txtPublicationsTitle.Text);

            return txtPublicationsTitle.Text;
        }

        public string setPricePublication(string number)
        {
            _publicationPrices = By.XPath("//li[" + number + "]//div[contains(@class,'ui-search-result__content-columns')]//div[contains(@class,'ui-search-price__second-line')]//span[contains(@class,'price-tag-text-sr-only')]");

            Console.WriteLine("EL PRECIO DE LA PUBLICACION ELEGIDA ES: " + txtPublicationsPrice.Text);

            return txtPublicationsPrice.Text;
        }

        public void openPublication(string number)
        {
            _publicationPrices = By.XPath("//li[" + number + "]//div[contains(@class,'ui-search-item__group ui-search-item__group--title')]/a/h2");

            txtPublicationsPrice.Click();
        }        

        // ESTE METODO ES PARA GENERAR UN NUMERO ALEATORIO EN BASE A LA CANTIDAD DE PUBLICACIONES
        //public int selectRandomNumber(int number)
        //{
        //    Random r = new Random();

        //    //Genera un numero entre 10 y 100 (101 no se incluye)
        //    Console.WriteLine("EL NUMERO DE PUBLICACIONES ES: " + number);
        //    Console.WriteLine("EL NUMERO ALEATORIO GENERADO ES: " + r.Next(1, number));

        //    int selectRandomNumber = r.Next(1, number);

        //    return selectRandomNumber;
        //}
    }
}
