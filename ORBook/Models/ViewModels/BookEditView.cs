using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ORBook.Models.ViewModels
{
    public class BookEditView
    {
        public Book Book { get; set; }
        public List<Genre>? Genres { get; set; }

        [Required]
        public List<int>? SelectedGenreIds { get; set; }
    }
    public class BookValidator : AbstractValidator<BookEditView>
    {
        public BookValidator()
        {
            RuleFor(b => b.Book.Name)
                .NotEmpty().WithMessage("Tên sách bắt buộc nhập!")
                .Length(4, 60).WithMessage("Tên sách phải từ 4 đến 60 ký tự!");
            RuleFor(b => b.Book.Description)
                .NotEmpty().WithMessage("Mô tả sách bắt buộc nhập!")
                .MinimumLength(4).WithMessage("Mô tả sách phải có trên 4 ký tự");
            RuleFor(b => b.Book.Author)
                .NotEmpty().WithMessage("Tên tác giả sách bắt buộc nhập!")
                .Length(4, 60).WithMessage("Tên tác giả sách phải từ 4 đến 60 ký tự!");
        }
    }
}
