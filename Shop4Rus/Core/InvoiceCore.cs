using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Enum;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core
{
   

     public class DiscountSystem
     {
      
        public readonly DiscountPoliciesMethods _discountPoliciesMethods;
       
        public readonly IMapper mapper;
        
        public readonly ILogger logger;

        public DiscountSystem(DiscountPoliciesMethods discountPolicy, IMapper Mapper, ILogger Logger)
        {
            _discountPoliciesMethods = discountPolicy;
            mapper = Mapper;
            logger = Logger;
        }

       
        public InvoiceAmount ComputePrice(TotalBill order)
        {

            var invoiceAmount = new InvoiceAmount();
            var GetDiscountByType = new DiscountCore(mapper,logger);
            var content = (int)System.Enum.Parse(typeof(User_Type),order.UserType);


            var GetDiscount = GetDiscountByType.GetDiscountByType(content);
            decimal nonDiscounted = order.orders.Sum(p => p.Price);
            decimal baseDiscount = _discountPoliciesMethods.BaseDiscount(order);
            decimal[] discounts = new[] {
            
                _discountPoliciesMethods.CustomeTypeDiscount(order,GetDiscount),
            
                _discountPoliciesMethods.CustomerLoyaltyDiscount(order),
       
            };
            decimal bestDiscount = discounts.Max(discount => discount);
            var totalDiscount = bestDiscount + baseDiscount;
            var  total = nonDiscounted - totalDiscount;
            invoiceAmount.InvoicePrice = total.ToString();
            invoiceAmount.DiscountCalculated = totalDiscount.ToString();
            invoiceAmount.UndiscountedPrice = nonDiscounted.ToString();
            logger.Information($"Onvoice to be paid  => {JsonConvert.SerializeObject(invoiceAmount)}");

            return invoiceAmount;
        }
  
     }


    public  class DiscountPoliciesMethods
    {

        public  decimal CustomeTypeDiscount(TotalBill order, ReturnMessage<Discounts_VM> GetDiscount)
        {

            
            if (order.UserType.ToString() == "AffliateCustomer")
            {
                return Convert.ToDecimal(GetDiscount.Body.DiscountPercentage) * order.orders.Sum(p => p.Price);
            }

            if (order.UserType.ToString() == "Staff")
            {
                return Convert.ToDecimal(GetDiscount.Body.DiscountPercentage) * order.orders.Sum(p => p.Price);
            }
            else 
            return 0m;
        }
        public  decimal CustomerLoyaltyDiscount(TotalBill order)
        {
            TimeSpan Span = DateTime.Now - order.DateCreated;
            if( (Span.TotalDays/365) > Convert.ToInt32(GetConfig.LoyalCustomerYears))
            {
                return (decimal)(Convert.ToInt32(GetConfig.LoyalCustomerPercent) * order.orders.Sum(p => p.Price));
            }
            else
                
             return 0m;
        }

        public decimal BaseDiscount(TotalBill order)
        {
            Decimal TotalAmount = order.orders.Sum(p => p.Price);
            int div = (int)TotalAmount / 100;
            return div *  Convert.ToInt32(GetConfig.BaseDiscount);

        }

    }
}
