namespace ModelSharingService.DTO
{
    public class UserRegistrationRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private string UserName { get; set; }
        public string password { get; set; }
    }
}
