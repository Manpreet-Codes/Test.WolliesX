using Answers.Models.Model;
using System.Threading.Tasks;

namespace Answers.BusinessLogic.Services.Interfaces.Trolley
{
    public interface ITrolleyCalculatorSpecialsProcessor
    {
        Task<decimal> ApplySpecialsToTrolleyProduct(Products trolleyProduct, decimal TotalAmount);
    }
}