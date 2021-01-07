using AutoMapper;
using NUnit.Framework;
using Serilog;
using Shop4Rus.Core;

namespace ShopTest
{
    public class Tests
    {
       private readonly ILogger logger;
        private readonly IMapper mapper;

      //public Tests()
       //{


//       }
       // public Tests(ILogger Logger,IMapper Mapper)
        //{
          //  logger =  Logger;
           // mapper = Mapper;
        //}


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestGetCustomer()
        {
  //          logger.

            var Customercore = new CustomerCore(mapper,logger);
            var CustomerDet = Customercore.GetCustomerByID(1);
            Assert.IsTrue(!string.IsNullOrEmpty(CustomerDet.Body.UserID));
        }
    }
}