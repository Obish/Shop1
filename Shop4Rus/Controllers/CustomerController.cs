using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Serilog;
using Shop4Rus.Models;
using Shop4Rus.Core;
using System.Net;
using Newtonsoft.Json;

namespace Shop4Rus.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly IMapper mapper;
        public readonly ILogger logger;

        public CustomerController(IMapper Mapper, ILogger Logger)
        {
            mapper = Mapper;
            logger = Logger;
        }


        /// <summary>
        /// 
        ///The ID of the Customer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The unique ID of the Customer</returns>
        [HttpGet]
        [Route("api/[controller]/GetCustomerByID/{ID}")]
        public ReturnMessage<Customers_VM> GetCustomerByID(int ID)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();
            try
            {
                if (ID > 0)
                {
                    var customerCore = new CustomerCore(mapper, logger);
                    GetCustomerResult = customerCore.GetCustomerByID(ID);
                    
                }

                else
                {
                    GetCustomerResult.ResponseCode = "01";
                    GetCustomerResult.ResponseDescription = "Valid ID is required";
                    GetCustomerResult.Body = null;
                }

            
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error retrieving Customer records");

                GetCustomerResult.ResponseCode = "96";
                GetCustomerResult.ResponseDescription = "Unable to retreive Customer records";
            }
            return GetCustomerResult;
        }
        [HttpGet]
        [Route("api/[controller]")]

        public ReturnMessage<List<Customers_VM>> GetAllCustomers()
        {
            var GetCustomerResult = new ReturnMessage<List<Customers_VM>>();
            try
            {
              
                    
                var customerCore = new CustomerCore(mapper, logger);
                
                GetCustomerResult = customerCore.GetCustomers();

            

            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error retrieving  records");

                GetCustomerResult.ResponseCode = "96";
                GetCustomerResult.ResponseDescription = "Unable to retreive  records";
            }
            return GetCustomerResult;
        }

        /// <summary>
        /// The Username unique to each customer
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/GetCustomerByUsername/{Username}")]

        public ReturnMessage<Customers_VM> GetCustomerByUsername(string Username)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();
            try
            {
                if (!string.IsNullOrEmpty(Username))
                {
                    var customerCore = new CustomerCore(mapper, logger);
                    GetCustomerResult = customerCore.GetCustomerByUsername(Username);

                }

                else
                {
                    GetCustomerResult.ResponseCode = "01";
                    GetCustomerResult.ResponseDescription = "Username is required";
                    GetCustomerResult.Body = null;
                }


            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error retrieving Customer records");

                GetCustomerResult.ResponseCode = "96";
                GetCustomerResult.ResponseDescription = "Unable to retreive Customer records";
            }
            return GetCustomerResult;
        }


        [HttpPost]
        [Route("api/[controller]")]
        public ReturnMessage<Customers_VM> Post([FromBody] Customers_VM Customer_Dets)
        {
            logger.Information($"Received request to CreateCustomer => {JsonConvert.SerializeObject(Customer_Dets)}");

            var CustomerCreateResult = new ReturnMessage<Customers_VM>();
            try
            {
                var customerCore = new CustomerCore(mapper, logger);
                if (ModelState.IsValid)
                {
                    CustomerCreateResult = customerCore.CreateCustomer(Customer_Dets);
                  
                    if (CustomerCreateResult.ResponseCode == "01")
                   
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    var errorList = (from item in ModelState.Values
                                     from error in item.Errors
                                     select error.ErrorMessage).ToArray();

                    CustomerCreateResult.ResponseDescription = String.Join('|', errorList);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;

                }


            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error creating Customer ");

                CustomerCreateResult.ResponseCode = "96";
                CustomerCreateResult.ResponseDescription = "Unable to create PassengerInfo";
            }
            return CustomerCreateResult;
        }

    }
}
