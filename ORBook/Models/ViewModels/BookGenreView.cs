using Microsoft.AspNetCore.Mvc.Rendering;

namespace ORBook.Models.ViewModels
{
    public class BookGenreView
    {
        public List<Book>? Books { get; set; }
        public SelectList? Genres { get; set; }
        public int? GenreId { get; set; }
        public string? SearchString { get; set; }
    }
}
