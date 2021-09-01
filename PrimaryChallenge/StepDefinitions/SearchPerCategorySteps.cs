using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PrimaryChallenge.PageObjects;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Interactions;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrimaryChallenge.StepDefinitions
{
    [Binding]
    public sealed class SearchPerCategorySteps
    {
        CategoriesPage categoriesPage;
        ResultsPage resultsPage;
        PublicationPage publicationPage;

        string savedProduct, titleOfPublication, priceOfPublication;

        IWebDriver webDriver = new ChromeDriver();


        [Given(@"I open the web page")]
        public void GivenIOpenTheWebPage()
        {
            webDriver.Navigate().GoToUrl("https://www.mercadolibre.com.ar/");
            webDriver.Manage().Window.Maximize();
            categoriesPage = new CategoriesPage(webDriver);
        }


        [Given(@"I search for products '(.*)' in category '(.*)'")]
        public void GivenISearchForProductsInCategory(string product, string category)
        {
            Actions actionProvider = new Actions(webDriver);

            actionProvider.MoveToElement(categoriesPage.btnCategories).Build().Perform();

            if (category == "Tecnología")
            {
                actionProvider.MoveToElement(categoriesPage.btnTechnology).Build().Perform();
                categoriesPage.TechCategory(product);
                categoriesPage.SearchInTechnology(product);
                savedProduct = product;
                Console.WriteLine("El producto trabajado es " + product + ", dentro de la categoría " + category);
            }
            else
            {
                categoriesPage.Search(product, category);
                savedProduct = product;
                Console.WriteLine("El producto trabajado es " + product + ", dentro de la categoría " + category);
            }
        }


        [Then(@"The results are displayed")]
        public void ThenTheResultsAreDisplayed()
        {

            resultsPage = new ResultsPage(webDriver);

            switch (savedProduct)
            {
                case "SAMSUNG":

                    Assert.IsTrue(resultsPage.IsListDisplayed("Samsung"));
                    Assert.IsTrue(resultsPage.txtResult.Text.Contains("Samsung"));
                    break;

                case "CAMIONES":

                    Assert.IsTrue(resultsPage.IsListDisplayed("Camiones"));
                    Assert.IsTrue(resultsPage.txtResult.Text.Contains("Camiones"));
                    break;

                case "OFICINAS":

                    Assert.IsTrue(resultsPage.IsListDisplayed("Oficinas"));
                    Assert.IsTrue(resultsPage.txtResult.Text.Contains("Oficinas"));
                    break;

                case "MUEBLES":

                    Assert.IsTrue(resultsPage.IsListDisplayed("Muebles"));
                    Assert.IsTrue(resultsPage.txtResult.Text.Contains("Muebles"));
                    break;

                case "AIRES AC.":

                    Assert.IsTrue(resultsPage.IsListDisplayed("Aires Ac"));
                    Assert.IsTrue(resultsPage.txtResult.Text.Contains("Aires Ac"));
                    break;
            }

            Assert.AreEqual(resultsPage.getNumberOfResults(), resultsPage.txtResultsAmount.Text);

            Console.WriteLine(resultsPage.getNumberOfResults());
            Console.WriteLine(resultsPage.txtResultsAmount.Text);

            webDriver.Close();
        }


        [Given(@"I search for '(.*)' in category '(.*)'")]
        public void GivenISearchForInCategory(string product, string category)
        {
            Actions actionProvider = new Actions(webDriver);

            actionProvider.MoveToElement(categoriesPage.btnCategories).Build().Perform();
            actionProvider.MoveToElement(categoriesPage.btnTechnology).Build().Perform();

            categoriesPage.TechCategory(product);

            Console.WriteLine("El producto trabajado es " + product + ", dentro de la categoría " + category);

            IJavaScriptExecutor js = webDriver as IJavaScriptExecutor;
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollBy(0,1400);");

            System.Threading.Thread.Sleep(5000);
        }


        [When(@"I filter the results by '(.*)' and choose the first product")]
        public void WhenIFilterTheResultsByAndChooseTheFirstProduct(string zone)
        {
            resultsPage = new ResultsPage(webDriver);

            Console.WriteLine("EL FILTRO APLICADO ES PARA LA ZONA DE: " + zone);

            resultsPage.applyFilterByZone(zone);

            System.Threading.Thread.Sleep(2000);

            // TOMAR EL NUMERO DE PUBLICACIONES Y ELEGIR UN NUMERO 

            //int numberOfPublications = resultsPage.getNumberOfPublications();
            //publicationNumber = resultsPage.selectRandomNumber(numberOfPublications);


            // TOME DE EJEMPLO EL PRIMER ELEMENTO DE LA LISTA QUE ESTÁ A LA VISTA

            titleOfPublication = resultsPage.setTitlePublication("1");
            priceOfPublication = resultsPage.setPricePublication("1");

            resultsPage.openPublication("1");
        }


        [Then(@"The title and price from the list matches with values in the publication")]
        public void ThenTheTitleAndPriceFromTheListMatchesWithValuesInThePublication()
        {

            publicationPage = new PublicationPage(webDriver);

            Console.WriteLine("EL TITULO ESPERADO ES: " + titleOfPublication);
            Console.WriteLine("EL TITULO ACTUAL ES: " + publicationPage.getActualTitle());

            Assert.AreEqual(titleOfPublication, publicationPage.getActualTitle());


            Console.WriteLine("EL PRECIO ESPERADO ES: " + priceOfPublication);
            Console.WriteLine("EL PRECIO ACTUAL ES: " + publicationPage.getActualPrice());

            string actualPrice = publicationPage.getActualPrice();

            Assert.IsTrue(actualPrice.Contains(priceOfPublication));

            webDriver.Close();
        }
    }
}
