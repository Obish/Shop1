using Shop4Rus.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Models
{
    public class DBmodels
    {
    }

    public class Customers
    {
        public string ID { get; set; }
        public string User_Name { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Phone_Number { get; set; }

        public string Email { get; set; }

        public int User_Type { get; set; }

        public string Email_Address { get; set; }

        public DateTime Date_Created { get; set; }
    }

    public class Discounts
    {
        public string Type { get; set; }

        public string Discount_Name { get; set; }

        public string Percentage { get; set; }

    }


}
