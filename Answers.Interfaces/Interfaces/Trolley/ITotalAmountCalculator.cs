using Answers.Models.Model;
using System.Threading.Tasks;

namespace Answers.BusinessLogic.Services.Interfaces.Trolley
{
    public interface ITotalAmountCalculator
    {
        Task<decimal> CalculateTotalAmountWithoutSpecials(Products trolleyProduct);
    }
}