using Answers.Extensions;
using Answers.Modal;
using NUnit.Framework;
using System.Collections.Generic;

namespace Answers.Services.Interfaces.ProductSorting.Tests
{
    [TestFixture()]
    public class ProductSortingServiceNameAscendingTests
    {
        private List<Product> products;
        private List<Product> out_products;

        private ProductSortingServiceNameAscending ProductSortingServiceNameAscending;

        [SetUp]
        public void SetUp()
        {
            products = new List<Product>();
            out_products = new List<Product>();

            products.Add(new Product() { Name = "Proct B", price = 0, quantity = 0 });
            products.Add(new Product() { Name = "Proct A", price = 0, quantity = 0 });
            products.Add(new Product() { Name = "Proct C", price = 0, quantity = 0 });

            out_products.Add(new Product() { Name = "Proct A", price = 0, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct B", price = 0, quantity = 0 });
            out_products.Add(new Product() { Name = "Proct C", price = 0, quantity = 0 });

            ProductSortingServiceNameAscending = new ProductSortingServiceNameAscending();
        }

        [Test()]
        public void SortProductDataTest()
        {
            var result = ProductSortingServiceNameAscending.SortProductData(products);
            Assert.IsTrue(result.Result.CompareProductList(out_products));
        }
    }
}