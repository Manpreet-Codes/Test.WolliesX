using Answers.Extensions;
using Answers.Modal;
using NUnit.Framework;
using System.Collections.Generic;

namespace Answers.Services.Interfaces.ProductSorting.Tests
{
    [TestFixture()]
    public class ProductSortingServiceHighPriceTests
    {
        private List<Product> products;
        private List<Product> out_products;

        private ProductSortingServiceHighPrice productSortingServiceHighPrice;

        [SetUp]
        public void SetUp()
        {
            products = new List<Product>();
            out_products = new List<Product>();

            products.Add(new Product() { Name = "Proct B", price = 10, quantity = 0 });
            products.Add(new Product() { Name = "Proct A", price = 20, quantity = 0 });
            products.Add(new Product() { Name = "Proct C", price = 30, quantity = 0 });

            out_products.Add(new Product() { Name = "Proct C", price = 30, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct A", price = 20, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct B", price = 10, quantity = 0 });

            productSortingServiceHighPrice = new ProductSortingServiceHighPrice();
        }

        [Test()]
        public void SortProductDataTest()
        {
            var result = productSortingServiceHighPrice.SortProductData(products);
            Assert.IsTrue(result.Result.CompareProductList(out_products));
        }
    }
}