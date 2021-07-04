using Answers.Modal;
using Answers.Services.Core.Data;
using Answers.Services.Core.ShoppingProcessor;
using Answers.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Answers.Services.Core.ProductSorting
{
    public class ProductSortingServiceNameRecommeded : IProductSortingServiceNameRecommeded
    {
        private readonly IHttpDataService _httpDataService;
        private readonly IShoppingHistoryProcessor _shoppingHistoryProcessor;
        private readonly string ShoppingHistoryUrl;

        public ProductSortingServiceNameRecommeded(IHttpDataService httpDataService,
            IShoppingHistoryProcessor shoppingHistoryProcessor, IOptions<AppSettings> settings)
        {
            _httpDataService = httpDataService;
            _shoppingHistoryProcessor = shoppingHistoryProcessor;
            ShoppingHistoryUrl = settings.Value.ShoppingHistoryUrl;
        }

        public async Task<List<Product>> SortProductData(List<Product> products)
        {
            List<Product> SortedProducts = new List<Product>();

            var shopppingHistory = await _httpDataService.GetRequest(ShoppingHistoryUrl);

            var res_prods = await _shoppingHistoryProcessor.ProcessShoppingHistoryForProductOccurance(shopppingHistory);

            var grp2 = res_prods.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();

            grp2.ForEach(x => SortedProducts.Add(products.First(z => z.name == x)));

            SortedProducts.AddRange(products.Except(SortedProducts));

            return SortedProducts.ToList();
        }
    }
}