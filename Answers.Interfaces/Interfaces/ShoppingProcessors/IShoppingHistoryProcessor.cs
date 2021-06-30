﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Answers.Services.Interfaces.ShoppingProcessor
{
    public interface IShoppingHistoryProcessor
    {
        Task<List<KeyValuePair<string, double>>> ProcessShoppingHistoryForProductOccurance(string shoppingHistory);
    }
}