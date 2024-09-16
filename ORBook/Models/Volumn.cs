using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORBook.Models
{
    public class Volumn
    {
        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        [StringLength(60)]
        public string? Title { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }
        [Display(Name = "Cập nhật cuối")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? UpdatedTime { get; set; }
        [ForeignKey("BookId")] // Chỉ định khóa ngoại
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
