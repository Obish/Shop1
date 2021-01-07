using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Utilities
{
 
    public static class DatabaseUtilities
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder();
        static Microsoft.AspNetCore.Http.HttpContext currentSession = new Microsoft.AspNetCore.Http.HttpContextAccessor().HttpContext;

        /// <summary>
        /// Creates and Returns Connection to an SQL Server Database 
        /// </summary>
        /// <param name="conString">Connection String of the SQL Server Database to create Connection to</param>
        /// <returns></returns>
        public static SqlConnection GetSQLConnection(string conString) => new SqlConnection(conString);

        ///// <summary>
        ///// Creates and Returns Connection to an Oracle Database 
        ///// </summary>
        ///// <param name="conString">Connection String of the Oracle Database to create Connection to</param>
        ///// <returns></returns>
        //public static OracleConnection GetOracleConnection(string conString) => new OracleConnection(conString);
    }
}
