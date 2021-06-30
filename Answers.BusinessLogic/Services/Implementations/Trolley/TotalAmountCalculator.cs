using Answers.BusinessLogic.Services.Interfaces.Trolley;
using Answers.Models.Model;
using System.Threading.Tasks;

namespace Answers.BusinessLogic.Services.Implementations.Trolley
{
    public class TotalAmountCalculator : ITotalAmountCalculator
    {
        public async Task<decimal> CalculateTotalAmountWithoutSpecials(Products trolleyProduct)
        {
            decimal totalPrice = 0;
            foreach (var tp in trolleyProduct.products)
            {
                foreach (var qu in trolleyProduct.quantities)
                {
                    if (tp.name.Equals(qu.name))
                    {
                        tp.quantity = qu.quantity;
                        totalPrice += (decimal)tp.price * qu.quantity;
                    }
                }
            }

            return totalPrice;
        }
    }
}