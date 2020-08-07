namespace Shortchase.Entities
{
    public class EmailConfig : IntBase
    {
        public string Display_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User_name { get; set; }
        public bool Enable_ssl { get; set; }
        public bool Is_default_email_account { get; set; }
        public bool? Active { get; set; }
    }
}