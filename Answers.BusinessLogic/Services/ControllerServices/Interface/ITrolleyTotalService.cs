using Answers.Models.Model;
using System.Threading.Tasks;

namespace Answers.Services.Service
{
    public interface ITrolleyTotalService
    {
        Task<decimal> CalculateTotal(Products products);
    }
}