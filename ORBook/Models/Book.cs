using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ORBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên sách")]
        [StringLength(60)]
        public string? Name { get; set; }
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "Tác giả")]
        public string? Author { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }
        [Display(Name = "Thể loại")]
        public List<BookGenre>? BookGenres { get; set; }
        [Display(Name = "Tập")]
        public List<Volumn>? Volumns { get; set; }
        [Display(Name = "Theo dõi")]
        public List<BookUser>? BookUsers { get; set; }
    }
}
