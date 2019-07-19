namespace DabeaV2.Common
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public SecurityStrings Security { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class SecurityStrings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public string PasswordSalt { get; set; }
        public int LoginExpires { get; set; }
    }
}
