using System;
using System.Collections.Generic;
using System.Linq;
namespace FoodDelivery.BL.DTOs.Address
{
    public class AddressGetDto
    {
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
