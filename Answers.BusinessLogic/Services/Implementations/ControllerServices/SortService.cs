using Answers.Modal;
using Answers.Services.Core.ProductSorting;
using Answers.Services.Core.ShoppingProcessors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Answers.Services.Service
{
    public class SortService : ISortService
    {
        private readonly IProductProcessor _productProcessor;
        private readonly IProductSortingServiceLowPrice _productSortingServiceLowPrice;
        private readonly IProductSortingServiceHighPrice _productSortingServiceHighPrice;
        private readonly IProductSortingServiceNameAscending _productSortingServiceNameAscending;
        private readonly IProductSortingServiceNameDescending _productSortingServiceNameDescending;
        private readonly IProductSortingServiceNameRecommeded _productSortingServiceNameRecommeded;

        public SortService(IProductProcessor productProcessor, IProductSortingServiceLowPrice productSortingServiceLowPrice,
            IProductSortingServiceHighPrice productSortingServiceHighPrice, IProductSortingServiceNameAscending productSortingServiceNameAscending,
            IProductSortingServiceNameDescending productSortingServiceNameDescending, IProductSortingServiceNameRecommeded productSortingServiceNameRecommeded)
        {
            _productProcessor = productProcessor;
            _productSortingServiceLowPrice = productSortingServiceLowPrice;
            _productSortingServiceHighPrice = productSortingServiceHighPrice;
            _productSortingServiceNameAscending = productSortingServiceNameAscending;
            _productSortingServiceNameDescending = productSortingServiceNameDescending;
            _productSortingServiceNameRecommeded = productSortingServiceNameRecommeded;
        }

        public async Task<List<Product>> PerformSort(string SortType)
        {
            List<Product> products = await _productProcessor.ProcessProducts();

            if (products != null && products.Count != 0)
            {
                if (SortType.Equals("low", StringComparison.InvariantCultureIgnoreCase))
                {
                    return await _productSortingServiceLowPrice.SortProductData(products);
                }
                else if (SortType.Equals("high", StringComparison.InvariantCultureIgnoreCase))
                {
                    return await _productSortingServiceHighPrice.SortProductData(products);
                }
                else if (SortType.Equals("Ascending", StringComparison.InvariantCultureIgnoreCase))
                {
                    return await _productSortingServiceNameAscending.SortProductData(products);
                }
                else if (SortType.Equals("Descending", StringComparison.InvariantCultureIgnoreCase))
                {
                    return await _productSortingServiceNameDescending.SortProductData(products);
                }
                else if (SortType.Equals("Recommended", StringComparison.InvariantCultureIgnoreCase))
                {
                    return await _productSortingServiceNameRecommeded.SortProductData(products);
                }
                else
                {
                    string error_json = "[{\"code\":\"400\",\"Message\":\"Bad Request Invalid Query String sortoption\"}]";
                    throw new HttpRequestException(error_json) { };
                }
            }
            else
            {
                string error_json = "[{\"code\":\"500\",\"Message\":\"No Product Information Available, Contact Admin or check App Settings\"}]";
                throw new HttpRequestException(error_json) { };
            }
        }
    }
}