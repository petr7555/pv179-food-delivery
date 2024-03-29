﻿namespace FoodDelivery.BL.DTOs.Address;

public class AddressCreateDto
{
    public string FullName { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
}
