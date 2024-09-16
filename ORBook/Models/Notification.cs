using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORBook.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        [Display(Name = "Người dùng")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("VolumnId")]
        [Display(Name = "Tập")]
        public int VolumnId { get; set; }
        public Volumn? Volumn { get; set; }

        [Display(Name = "Đã đọc")]
        public bool IsRead { get; set; } = false;

        [Required]
        [Display(Name = "Thời gian tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
