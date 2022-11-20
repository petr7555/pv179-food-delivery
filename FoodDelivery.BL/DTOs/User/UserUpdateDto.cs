namespace FoodDelivery.BL.DTOs.User
{
    public class UserUpdateDto
    {
        public string Username { get; set; }

        public bool Banned { get; set; }

        public int RoleId { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
