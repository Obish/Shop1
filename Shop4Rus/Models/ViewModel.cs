using Shop4Rus.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Models
{
    public class ViewModel
    {

    }

    public class Customers_VM
    {
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public User_Type UserType { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class Discounts_VM
    {
        public string DiscountType { get; set; }

        public string DiscountName { get; set; }

        public string DiscountPercentage { get; set; }

    }

    public class TotalBill
    {
        public int UserId { get; set; }

        public string UserType { get; set; }

        public DateTime DateCreated { get; set; }

        public List<Orders_Details> orders { get; set; } 
    }

    public class Orders_Details
    {
        public string  PurchaseName { get; set; }
        public long Price { get; set; }

        public string ProductCategory  { get; set; }

    }

    public class InvoiceAmount
    {
        public string InvoicePrice { get; set; }

        public string UndiscountedPrice { get; set; }

        public string DiscountCalculated { get; set; }


    }
    public class ReturnMessage<T>
    {
        public virtual string Id { get; set; }

        public virtual string ResponseCode { get; set; }
    
        public virtual string ResponseDescription { get; set; }      
  
        public virtual T Body { get; set; }


    }
}
