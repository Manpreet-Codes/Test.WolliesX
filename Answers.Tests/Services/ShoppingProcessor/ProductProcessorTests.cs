using Answers.Extensions;
using Answers.Modal;
using Answers.Services.Interfaces.Data;
using Answers.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Answers.Services.Interfaces.ShoppingProcessors.Tests
{
    [TestFixture()]
    public class ProductProcessorTests
    {
        private Mock<IHttpDataService> httpDataServiceMock;
        private Mock<IOptions<AppSettings>> settings;
        private List<Product> products;
        private ProductProcessor productProcessor;

        [SetUp]
        public void SetUp()
        {
            httpDataServiceMock = new Mock<IHttpDataService>();
            settings = new Mock<IOptions<AppSettings>>();

            settings.Setup(p => p.Value).Returns(new AppSettings()
            {
                ShoppingHistoryUrl = "Dummy String"
            });

            products = new List<Product>();
            string productjsonstring = "[{\"name\":\"Test Product A\",\"price\":99.99,\"quantity\":3.0}]";
            products.Add(new Product() { name = "Test Product A", price = 99.99, quantity = 3.0 });

            httpDataServiceMock.Setup(p => p.GetRequest(It.IsAny<string>())).Returns(Task.FromResult(productjsonstring));

            productProcessor = new ProductProcessor(httpDataServiceMock.Object, settings.Object);
        }

        [Test()]
        public void ProcessProductsTest()
        {
            var prods = productProcessor.ProcessProducts();

            Assert.IsTrue(prods.Result.CompareProductList(products));
        }
    }
}