using System.ComponentModel;

namespace ORBook.Models.ViewModels
{
    public class RegisterView
    {
        [DisplayName("Tên")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
    }
}
