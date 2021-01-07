using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shop4Rus.Core;
using Shop4Rus.Models;

namespace Shop4Rus.Controllers
{
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DiscountController (IMapper Mapper, ILogger Logger)
        {
            mapper = Mapper;
            logger = Logger;
        }
        /// <summary>
        /// The unique identifier for each discount type
        /// </summary>
        /// <param name="Type"></param>
        /// <returns>The unique identifier for each discount type</returns>
        [HttpGet]
        [Route("api/[controller]/GetDiscountByType/{Type}")]

        public ReturnMessage<Discounts_VM> GetDiscountByType(int Type)
        {
            var GetDiscount = new ReturnMessage<Discounts_VM>();
            try
            {
                if (Type > 0)
                {
                    var discountCore = new DiscountCore(mapper, logger);
                    GetDiscount = discountCore.GetDiscountByType(Type);

                }

                else
                {
                    GetDiscount.ResponseCode = "01";
                    GetDiscount.ResponseDescription = "Valid ID is required";
                    GetDiscount.Body = null;
                }


            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error retrieving Customer records");

                GetDiscount.ResponseCode = "96";
                GetDiscount.ResponseDescription = "Unable to retreive Customer records";
            }
            return GetDiscount;
        }
        [HttpGet]
        [Route("api/[controller]")]

        public ReturnMessage<List<Discounts_VM>> GetAllDiscounts()
        {
            var GetDiscount = new ReturnMessage<List<Discounts_VM>>();
            try
            {
                
                var discountCore = new DiscountCore(mapper, logger);
                
                GetDiscount = discountCore.GetAllDiscounts();

                



            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error retrieving Customer records");

                GetDiscount.ResponseCode = "96";
                GetDiscount.ResponseDescription = "Unable to retreive Customer records";
            }
            return GetDiscount;
        }



        [HttpPost]
        [Route("api/[controller]")]
        public ReturnMessage<Discounts_VM> Post([FromBody] Discounts_VM discounts)
        {
            var CreateDiscount = new ReturnMessage<Discounts_VM>();
            try
            {
                var discountCore = new DiscountCore(mapper, logger);
                if (ModelState.IsValid)
                {
                    CreateDiscount = discountCore.CreateNewDiscount(discounts);

                    if (CreateDiscount.ResponseCode == "01")

                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    var errorList = (from item in ModelState.Values
                                     from error in item.Errors
                                     select error.ErrorMessage).ToArray();

                    CreateDiscount.ResponseDescription = String.Join('|', errorList);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;

                }


            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error creating Customer ");

                CreateDiscount.ResponseCode = "96";
                CreateDiscount.ResponseDescription = "Unable to create PassengerInfo";
            }
            return CreateDiscount;
        }
    }
    
}
