using HomeIncClient.Core.Store;

namespace HomeIncClient.Models
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}