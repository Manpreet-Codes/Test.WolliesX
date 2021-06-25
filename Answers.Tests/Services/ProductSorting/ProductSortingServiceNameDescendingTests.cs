using Answers.Extensions;
using Answers.Modal;
using NUnit.Framework;
using System.Collections.Generic;

namespace Answers.Services.Interfaces.ProductSorting.Tests
{
    [TestFixture()]
    public class ProductSortingServiceNameDescendingTests
    {
        private List<Product> products;
        private List<Product> out_products;

        private ProductSortingServiceNameDescending ProductSortingServiceNameDescending;

        [SetUp]
        public void SetUp()
        {
            products = new List<Product>();
            out_products = new List<Product>();

            products.Add(new Product() { name = "Proct B", price = 10, quantity = 0 });
            products.Add(new Product() { name = "Proct A", price = 20, quantity = 0 });
            products.Add(new Product() { name = "Proct C", price = 30, quantity = 0 });

            out_products.Add(new Product() { name = "Proct C", price = 30, quantity = 0 });
            out_products.Add(new Product() { name = "Proct B", price = 10, quantity = 0 });
            out_products.Add(new Product() { name = "Proct A", price = 20, quantity = 0 });

            ProductSortingServiceNameDescending = new ProductSortingServiceNameDescending();
        }

        [Test()]
        public void SortProductDataTest()
        {
            var result = ProductSortingServiceNameDescending.SortProductData(products);
            Assert.IsTrue(result.Result.CompareProductList(out_products));
        }
    }
}