﻿using Answers.Modal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Answers.Services.Interfaces.ProductSorting
{
    public class ProductSortingServiceNameDescending : IProductSortingServiceNameDescending
    {
        public async Task<List<Product>> SortProductData(List<Product> products)
        {
            return products.OrderByDescending(x => x.name).ToList();
        }
    }
}