using Answers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Answers.Services.Interfaces.ShoppingProcessor.Tests
{
    [TestFixture()]
    public class ShoppingHistoryProcessorTests
    {
        private string productjsonstring;
        private List<KeyValuePair<string, double>> out_lst;

        private ShoppingHistoryProcessor shoppingHistoryProcessor;

        [SetUp]
        public void SetUp()
        {
            out_lst = new List<KeyValuePair<string, double>>();
            shoppingHistoryProcessor = new ShoppingHistoryProcessor();
            out_lst.Add(new KeyValuePair<string, double>("Test Product A", 3));
            productjsonstring = "[{\"customerId\":123,\"products\":[{\"name\":\"Test Product A\",\"price\":99.99,\"quantity\":3.0}]}]";
        }

        [Test()]
        public void ProcessShoppingHistoryForProductOccuranceTest()
        {
            var result = shoppingHistoryProcessor.ProcessShoppingHistoryForProductOccurance(productjsonstring);
            Assert.IsTrue(result.Result.CompareKeyValue(out_lst));
        }
    }
}