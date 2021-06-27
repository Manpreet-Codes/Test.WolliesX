using Answers.Extensions;
using Answers.Modal;
using Answers.Services.Interfaces.Data;
using Answers.Services.Interfaces.ShoppingProcessor;
using Answers.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Answers.Services.Interfaces.ProductSorting.Tests
{
    [TestFixture()]
    public class ProductSortingServiceNameRecommededTests
    {
        private ProductSortingServiceNameRecommeded productSortingServiceNameRecommeded;
        private Mock<IHttpDataService> httpDataServiceMock;
        private Mock<IShoppingHistoryProcessor> shoppingHistoryProcessorMock;
        private List<KeyValuePair<string, double>> out_lst;
        private Mock<IOptions<AppSettings>> settings;

        private List<Product> products;
        private List<Product> out_products;

        [SetUp]
        public void SetUp()
        {
            httpDataServiceMock = new Mock<IHttpDataService>();
            settings = new Mock<IOptions<AppSettings>>();
            shoppingHistoryProcessorMock = new Mock<IShoppingHistoryProcessor>();
            products = new List<Product>();
            out_products = new List<Product>();

            settings.Setup(p => p.Value).Returns(new AppSettings() { 
            ShoppingHistoryUrl = "Dummy String"} );
           

            out_lst = new List<KeyValuePair<string, double>>();
            //Setup Data
            out_lst.Add(new KeyValuePair<string, double>("Proct A", 9));
            out_lst.Add(new KeyValuePair<string, double>("Proct B", 7));
            //out_lst.Add(new KeyValuePair<string, double>("Proct C", 9));

            products.Add(new Product() { Name = "Proct B", price = 0, quantity = 0 });
            products.Add(new Product() { Name = "Proct A", price = 0, quantity = 0 });
            products.Add(new Product() { Name = "Proct C", price = 0, quantity = 0 });

            out_products.Add(new Product() { Name = "Proct A", price = 0, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct B", price = 0, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct C", price = 0, quantity = 0 });

            string productjsonstring = "[{\"customerId\":123,\"products\":[{\"name\":\"Test Product A\",\"price\":99.99,\"quantity\":3.0}]}]";

            
            httpDataServiceMock.Setup(p => p.GetRequest(It.IsAny<string>())).Returns(Task.FromResult(productjsonstring));
            shoppingHistoryProcessorMock.
                Setup(p => p.ProcessShoppingHistoryForProductOccurance(It.IsAny<string>())).Returns(Task.FromResult(out_lst));

            productSortingServiceNameRecommeded =
            new ProductSortingServiceNameRecommeded(httpDataServiceMock.Object, shoppingHistoryProcessorMock.Object, settings.Object);
        }

        [Test()]
        public void SortProductDataTest()
        {
            string productjsonstring = "[{\"name\":\"Test Product A\",\"price\":99.99,\"quantity\":3.0}]";

            var result = productSortingServiceNameRecommeded.SortProductData(products);
            Assert.IsTrue(result.Result.CompareProductList(out_products));
        }
    }
}