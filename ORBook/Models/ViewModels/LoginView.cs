using System.ComponentModel;

namespace ORBook.Models.ViewModels
{
    public class LoginView
    {
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
