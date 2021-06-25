﻿using Answers.Modal;
using Answers.Services.Interfaces.Data;
using Answers.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Answers.Services.Interfaces.ShoppingProcessors
{
    public class ProductProcessor : IProductProcessor
    {
        private readonly IHttpDataService _httpDataService;
        private string ProductsUrl;
        public ProductProcessor(IHttpDataService httpDataService, IOptions<AppSettings> settings)
        {
            _httpDataService = httpDataService;
            ProductsUrl = settings.Value.ProductsUrl;
        }

        public async Task<List<Product>> ProcessProducts()
        {
           
            var result = await _httpDataService.GetRequest(ProductsUrl);

            var arr_res = JsonSerializer.Deserialize<Product[]>(result);

            return arr_res.ToList();
            
        }
    }
}