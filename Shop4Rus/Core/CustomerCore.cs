using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core
{
    public class CustomerCore
    {
        public readonly IMapper mapper;
        public readonly ILogger logger;

        public CustomerCore(IMapper Mapper, ILogger Logger)
        {
            mapper = Mapper;
            logger = Logger;
        }

        public ReturnMessage<Customers_VM> GetCustomerByID (int ID)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@ID",ID.ToString() }
            };
            var CustomerResult = Repo<Customers>.GetObjectNoParam(dbConnection, paras, "proc_tblCustomers_GetCustomerByID");
            logger.Information($"Response from DB  to get  Customer By ID => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null)
            {
                var CustomerResult_VM = mapper.Map<Customers, Customers_VM>(CustomerResult);
                CustomerResult_VM.UserType = (Enum.User_Type)(int)CustomerResult.User_Type;
                GetCustomerResult.Body = CustomerResult_VM;
                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;

        }

        public ReturnMessage<Customers_VM> GetCustomerByUsername(string Username)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@User_Name",Username }
            };
            var CustomerResult = Repo<Customers>.GetObjectNoParam(dbConnection, paras, "proc_tblCustomers_GetCustomerByUserName");
            logger.Information($"Response from DB  to get  Customer By username => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null)
            {
                var CustomerResult_VM = mapper.Map<Customers, Customers_VM>(CustomerResult);
                GetCustomerResult.Body = CustomerResult_VM;
                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;

        }

        public ReturnMessage<List<Customers_VM>> GetCustomers()
        {
            var GetCustomerResult = new ReturnMessage<List<Customers_VM>>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
           
            var CustomerResult = Repo<Customers>.GetListNoParam(dbConnection, "proc_tblCustomers_GetAllCustomers");
            logger.Information($"Response from DB  to get all  Customers => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null && CustomerResult.Count > 0)
            {
                List<Customers_VM> CustomerResult_VM = mapper.Map<List<Customers>, List<Customers_VM>>(CustomerResult);
                GetCustomerResult.Body = CustomerResult_VM;
                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;
        }

        public ReturnMessage<Customers_VM> CreateCustomer(Customers_VM customer)
        {
            var CreateCustomer = new ReturnMessage<Customers_VM>();

            var Creatusermap = mapper.Map<Customers_VM, Customers>(customer);
            Creatusermap.User_Type = (int)customer.UserType;
            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@Email",Creatusermap.Email },
                
                { "@User_Name",Creatusermap.User_Name },
                
                { "@First_Name",Creatusermap.First_Name },
                
                { "@Last_Name",Creatusermap.Last_Name },
                
                { "@Phone_Number",Creatusermap.Phone_Number },
                
                { "@User_Type",Creatusermap.User_Type.ToString() },
            };


            var  returnMessage = Repo<ReturnMessage<Customers_VM>>.GetObject(dbConnection, paras, "proc_tblCustomers_AddCustomer",
                CommandType.StoredProcedure);
            logger.Information($"Response from DB  to create  Customer  => {JsonConvert.SerializeObject(returnMessage)}");

            if (returnMessage.ResponseCode == "1")
            {
                CreateCustomer.ResponseCode = "00";
                CreateCustomer.ResponseDescription = "Success";
                CreateCustomer.Body = customer;             
                CreateCustomer.Body.UserID = returnMessage.Id;

            }
            else if (returnMessage.ResponseCode == "2")
            {
                CreateCustomer.ResponseCode = "01";
                CreateCustomer.ResponseDescription =$"Unable to save record => {returnMessage.ResponseDescription}" ;
            }

            else
            {
                CreateCustomer.ResponseDescription = "96";
                CreateCustomer.ResponseDescription = "Unable to save customer Records";
            }

            return CreateCustomer;


        }


    }
}
