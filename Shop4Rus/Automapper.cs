using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Shop4Rus.Models;

namespace Shop4Rus
{
    public class Automapper: Profile
    {
        public Automapper()
        {
            CreateMap<Customers, Customers_VM>()
                             
                
              .ForMember(dest => dest.UserID, bank => bank.MapFrom(src => src.ID))

              .ForMember(dest => dest.Email, bank => bank.MapFrom(src => src.Email_Address))
              .ForMember(dest => dest.UserName, bank => bank.MapFrom(src => src.User_Name))
              .ForMember(dest => dest.FirstName, bank => bank.MapFrom(src => src.First_Name))
              .ForMember(dest => dest.LastName, bank => bank.MapFrom(src => src.Last_Name))
              .ForMember(dest => dest.DateCreated, bank => bank.MapFrom(src => src.Date_Created))

              .ForMember(dest => dest.PhoneNumber, bank => bank.MapFrom(src => src.Phone_Number))
                           

          .ReverseMap();



            CreateMap<Discounts,Discounts_VM>()
                 .ForMember(dest => dest.DiscountType, bank => bank.MapFrom(src => src.Type))

              .ForMember(dest => dest.DiscountName, bank => bank.MapFrom(src => src.Discount_Name))
              .ForMember(dest => dest.DiscountPercentage, bank => bank.MapFrom(src => src.Percentage))
                        .ReverseMap();


        }
    }
}
