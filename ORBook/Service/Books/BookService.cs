using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;

namespace ORBook.Service.Books
{
    public class BookService : ICommonDataService<Book>
    {
        private readonly ORBookContext _orbookcontext;
        public BookService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }
        public async Task<Book> CreateAsync(Book book)
        {
            book.CreatedTime = DateTime.Now;
            _orbookcontext.Book.Add(book);
            await _orbookcontext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _orbookcontext.Book.FindAsync(id);
            if (book != null)
            {
                _orbookcontext.Remove(book);
                await _orbookcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _orbookcontext.Book.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _orbookcontext.Book
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookUsers)
                .ThenInclude(bu => bu.User)

                .Include(b => b.Volumns)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            if (await _orbookcontext.Book.AnyAsync(e => e.Id == book.Id))
            {
                _orbookcontext.Book.Update(book);
                await _orbookcontext.SaveChangesAsync();
                return book;
            }
            return null;
        }
    }
}
