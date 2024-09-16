using System.ComponentModel.DataAnnotations;

namespace ORBook.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Tên người dùng")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Quyền")]
        public string? role { get; set; } = "";
        [Display(Name = "Sách")]
        public List<BookUser>? BookUsers { get; set; }
    }
}
