using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ORBook.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Display(Name = "Tên thể loại")]
        [StringLength(60)]
        public string? Name { get; set; }
        [Display(Name = "Sách")]
        public List<BookGenre>? BookGenres { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
