
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ORBook.Service.BookGenres
{
    public class BookGenreService : IBookGenreService
    {
        private readonly ORBookContext _orbookcontext;
        public BookGenreService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }

        public async Task<bool> CreateAsync(int bookId, List<int> genreId)
        {
            var book = await _orbookcontext.Book
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return false;
            }

            if (genreId != null && genreId.Any())
            {
                foreach (var GenreId in genreId)
                {
                    var bookGenre = new BookGenre
                    {
                        BookId = bookId,
                        GenreId = GenreId
                    };
                    _orbookcontext.BookGenre.Add(bookGenre);
                }
            }

            await _orbookcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int bookId)
        {
            var book = await _orbookcontext.Book
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return false;
            }

            _orbookcontext.BookGenre.RemoveRange(book.BookGenres);
            return true;
        }

        public async Task<List<Book>> GetSearchPage(int genreId, string searchString, int Page, int PageSize)
        {
            var book = _orbookcontext.Book.AsQueryable();

            if (genreId > 0)
            {
                book = book.Where(b => b.BookGenres!.Any(bg => bg.GenreId == genreId));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                book = book.Where(b => b.Name!.ToLower().Contains(searchString.ToLower()));
            }

            if(Page > 0)
            {
                book = book.Skip((Page - 1) * PageSize).Take(PageSize);
            }

            return await book.ToListAsync();
        }

        public async Task<bool> UpdateAsync(int bookId, List<int> genreId)
        {
            var book = await _orbookcontext.Book
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return false;
            }

            _orbookcontext.BookGenre.RemoveRange(book.BookGenres);

            if (genreId != null && genreId.Any())
            {
                foreach (var GenreId in genreId)
                {
                    var bookGenre = new BookGenre
                    {
                        BookId = bookId,
                        GenreId = GenreId
                    };
                    _orbookcontext.BookGenre.Add(bookGenre);
                }
            }

            await _orbookcontext.SaveChangesAsync();

            return true;
        }
    }
}
