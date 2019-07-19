namespace DabeaV2.ViewModels
{
    public class UserViewvModel
    {
        public long? Id { get; set; }
        public bool IsExtern { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
