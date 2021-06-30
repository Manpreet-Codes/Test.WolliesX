using Answers.BusinessLogic.Services.Interfaces.Trolley;
using Answers.Modal;
using Answers.Models.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Answers.BusinessLogic.Services.Implementations.Trolley
{
    public class TrolleyCalculatorSpecialsProcessor : ITrolleyCalculatorSpecialsProcessor
    {
        public async Task<decimal> ApplySpecialsToTrolleyProduct(Products trolleyProduct, decimal TotalAmount)
        {
            var specialsToApply = await GetSpecialsToApplySortedByHighestValue(trolleyProduct);

            foreach (var sp in specialsToApply)
            {
                TotalAmount = await ApplySpecialToTrolleyProducts(sp, trolleyProduct, TotalAmount);
            }

            return TotalAmount;
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