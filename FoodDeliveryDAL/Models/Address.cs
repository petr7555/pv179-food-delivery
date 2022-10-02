namespace FoodDeliveryDAL.Models
{
    public class Address : BaseEntity
    {
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}