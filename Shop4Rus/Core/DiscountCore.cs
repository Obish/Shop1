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
    public class DiscountCore
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DiscountCore( IMapper Mapper,ILogger Logger)
        {
            logger = Logger;
            mapper = Mapper;
        }

        public ReturnMessage<Discounts_VM> GetDiscountByType(int Type)
        {
            var GetDiscount =new ReturnMessage<Discounts_VM>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@Type",Type.ToString() }
            };
            var DiscountResult = Repo<Discounts>.GetObjectNoParam(dbConnection, paras, "proc_tblDiscounts_GetDiscountByType");
            logger.Information($"Response from DB  to get  Discounts By Type => {JsonConvert.SerializeObject(DiscountResult)}");

            if (DiscountResult != null )
            {
                var DiscountResult_VM = mapper.Map<Discounts, Discounts_VM>(DiscountResult);
                GetDiscount.Body = DiscountResult_VM;
                GetDiscount.ResponseCode = "00";
                GetDiscount.ResponseDescription = "Success";
            }

            else
            {
                GetDiscount.ResponseCode = "25";
                GetDiscount.ResponseDescription = "No result found";

            }
            return GetDiscount;
        }

        public ReturnMessage<List<Discounts_VM>> GetAllDiscounts()
        {
            var GetDiscounts = new ReturnMessage<List<Discounts_VM>>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);

            var DiscountResult = Repo<Discounts>.GetListNoParam(dbConnection, "proc_tblDiscounts_GetAllDiscounts");
            logger.Information($"Response from DB  to get all Discounts => {JsonConvert.SerializeObject(DiscountResult)}");

            if (DiscountResult != null && DiscountResult.Count > 0)
            {
                List<Discounts_VM> DiscountResult_VM = mapper.Map<List<Discounts>, List<Discounts_VM>>(DiscountResult);
                GetDiscounts.Body = DiscountResult_VM;
                GetDiscounts.ResponseCode = "00";
                GetDiscounts.ResponseDescription = "Success";
            }

            else
            {
                GetDiscounts.ResponseCode = "25";
                GetDiscounts.ResponseDescription = "No result found";

            }
            return GetDiscounts;
        }

        public ReturnMessage<Discounts_VM> CreateNewDiscount(Discounts_VM discount)
        {
            var CreateDiscount = new ReturnMessage<Discounts_VM>();

            var CreateDiscountmap = mapper.Map<Discounts_VM, Discounts>(discount);

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@Discount_Name", CreateDiscountmap.Discount_Name},

                { "@Percentage",CreateDiscountmap.Percentage },

            };


            var returnMessage = Repo<ReturnMessage<Discounts_VM>>.GetObject(dbConnection, paras, "proc_tblDiscounts_AddDiscount",
                CommandType.StoredProcedure);
            logger.Information($"Response from DB  to Creat new  Discounts  => {JsonConvert.SerializeObject(returnMessage)}");

            if (returnMessage.ResponseCode == "1")
            {
                CreateDiscount.ResponseCode = "00";
                CreateDiscount.ResponseDescription = "Success";
                CreateDiscount.Body = discount;
                CreateDiscount.Body.DiscountType = returnMessage.Id;

            }
            else if (returnMessage.ResponseCode == "2")
            {
                CreateDiscount.ResponseCode = "01";
                CreateDiscount.ResponseDescription = $"Unable to save record => {returnMessage.ResponseDescription}";
            }

            else
            {
                CreateDiscount.ResponseDescription = "96";
                CreateDiscount.ResponseDescription = "Unable to save customer Records";
            }

            return CreateDiscount;


        }

    }
}
