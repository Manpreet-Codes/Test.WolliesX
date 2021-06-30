using Answers.BusinessLogic.Services.ControllerServices.Implementations;
using Answers.BusinessLogic.Services.Implementations.Trolley;
using Answers.BusinessLogic.Services.Interfaces.Trolley;
using Answers.Services.Implementations;
using Answers.Services.Interfaces;
using Answers.Services.Interfaces.Data;
using Answers.Services.Interfaces.ProductSorting;
using Answers.Services.Interfaces.ShoppingProcessor;
using Answers.Services.Interfaces.ShoppingProcessors;
using Answers.Services.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Answers.Extensions
{
    public static class EntityBuilder
    {
        public static IServiceCollection RegisterAppObjects(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHttpDataService, HttpDataService>();
            services.AddScoped<IHttpClientServiceGet, HttpClientServiceGet>();
            services.AddScoped<IHttpClientServicePost, HttpClientServicePost>();
            services.AddScoped<IProductSortingServiceHighPrice, ProductSortingServiceHighPrice>();
            services.AddScoped<IProductSortingServiceLowPrice, ProductSortingServiceLowPrice>();
            services.AddScoped<IProductSortingServiceNameAscending, ProductSortingServiceNameAscending>();
            services.AddScoped<IProductSortingServiceNameDescending, ProductSortingServiceNameDescending>();
            services.AddScoped<IProductSortingServiceNameRecommeded, ProductSortingServiceNameRecommeded>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISortService, SortService>();
            services.AddScoped<ITrolleyTotalService, TrolleyTotalService>();
            services.AddScoped<IShoppingHistoryProcessor, ShoppingHistoryProcessor>();
            services.AddScoped<IProductProcessor, ProductProcessor>();
            services.AddScoped<ITotalAmountCalculator, TotalAmountCalculator>();
            services.AddScoped<ITrolleyCalculatorSpecialsProcessor, TrolleyCalculatorSpecialsProcessor>();
            return services;
        }
    }
}