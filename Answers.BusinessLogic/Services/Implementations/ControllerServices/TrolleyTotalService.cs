using Answers.BusinessLogic.Services.Interfaces.Trolley;
using Answers.Modal;
using Answers.Models.Model;
using Answers.Services.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Answers.BusinessLogic.Services.ControllerServices.Implementations
{
    public class TrolleyTotalService : ITrolleyTotalService
    {
        private readonly ITotalAmountCalculator _totalAmountCalculator;
        private readonly ITrolleyCalculatorSpecialsProcessor _trolleyCalculatorSpecialsProcessor;

        public TrolleyTotalService(ITotalAmountCalculator totalAmountCalculator, ITrolleyCalculatorSpecialsProcessor trolleyCalculatorSpecialsProcessor)
        {
            _totalAmountCalculator = totalAmountCalculator;
            _trolleyCalculatorSpecialsProcessor = trolleyCalculatorSpecialsProcessor;
        }

        public async Task<decimal> CalculateTotal(Products trolleyProduct)
        {
            var TotalAmount = await _totalAmountCalculator.CalculateTotalAmountWithoutSpecials(trolleyProduct);

            decimal org_TotalAmount = TotalAmount;

            TotalAmount = await _trolleyCalculatorSpecialsProcessor.ApplySpecialsToTrolleyProduct(trolleyProduct, TotalAmount);

            return org_TotalAmount > TotalAmount ? TotalAmount : org_TotalAmount;
        }

        private async Task<decimal> CalculateTotalAmountWithoutSpecials(Products trolleyProduct)
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

        private async Task<List<Special>> GetSpecialsToApplySortedByHighestValue(Products trolleyProduct)
        {
            List<Special> specialsToApply = new List<Special>();

            bool bFound = false;

            foreach (var sp in trolleyProduct.specials)
            {
                foreach (var qu in sp.quantities)
                {
                    bFound = false;
                    foreach (var tp in trolleyProduct.products)
                    {
                        if (tp.name.Equals(qu.name))
                        {
                            bFound = true;
                            break;
                        }
                    }

                    if (bFound == false)
                        break;
                }

                if (bFound == true)
                {
                    specialsToApply.Add(sp);
                }
            }

            return specialsToApply;
        }

        private async Task<decimal> ApplySpecialsToTrolleyProduct(Products trolleyProduct, List<Special> specialsToApply, decimal TotalAmount)
        {
            foreach (var sp in specialsToApply)
            {
                TotalAmount = await ApplySpecialToTrolleyProducts(sp, trolleyProduct, TotalAmount);
            }

            return TotalAmount;
        }

        private async Task<bool> IsSpecialQuantityStillApply(Special special, List<Product> products)
        {
            bool bFound = false;

            foreach (var qu in special.quantities)
            {
                bFound = false;
                foreach (var tp in products)
                {
                    if (tp.name.Equals(qu.name) && tp.quantity >= qu.quantity)
                    {
                        bFound = true;
                        break;
                    }
                }
            }
            return bFound;
        }

        private async Task<decimal> ApplySpecialToTrolleyProducts(Special special, Products trolleyProduct, decimal TotalAmount)
        {
            decimal priceToAdd = 0;
            while (await IsSpecialQuantityStillApply(special, trolleyProduct.products.ToList()))
            {
                foreach (var qu in special.quantities)
                {
                    decimal priceToSubs = 0;
                    priceToAdd = special.total;
                    foreach (var prd in trolleyProduct.products)
                    {
                        if (prd.name.Equals(qu.name))
                        {
                            if (qu.quantity <= prd.quantity)
                            {
                                prd.quantity -= qu.quantity;
                                priceToSubs += qu.quantity * (decimal)prd.price;
                            }
                        }
                    }
                    TotalAmount -= priceToSubs;
                }
                TotalAmount += priceToAdd;
            }

            return TotalAmount;
        }
    }
}