using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Utilities
{
    public class GetConfig
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder();
        static IConfigurationRoot _ConfigRoot;
        static Microsoft.AspNetCore.Http.HttpContext currentSession = new Microsoft.AspNetCore.Http.HttpContextAccessor().HttpContext;

        private static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot;
        }
        static private IConfigurationRoot GetAppSettings()
        {
            var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            .SetBasePath(ApplicationExeDirectory())
            .AddJsonFile("appsettings.json");
            if (_ConfigRoot == null)
            {
                _ConfigRoot = builder.Build();
            }
            return _ConfigRoot;
        }

        public static string ConnectionString => GetAppSettings()["ConnectionString:DBconn"];
        public static string LoyalCustomerPercent => GetAppSettings()["Discounts:LoyalCustomerPercent"];

        public static string LoyalCustomerYears => GetAppSettings()["Discounts:LoyalCustomerYears"];

        public static string BaseDiscount => GetAppSettings()["Discounts:BaseDiscount"];

    }
}
