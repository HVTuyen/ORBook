using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;
using System.Net;

namespace ORBook.Service.Genres
{
    public class GenreService : ICommonDataService<Genre>
    {
        private readonly ORBookContext _orbookcontext;
        public GenreService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }
        public async Task<Genre> CreateAsync(Genre genre)
        {
            _orbookcontext.Genre.Add(genre);
            await _orbookcontext.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre =  await _orbookcontext.Genre.FindAsync(id);
            if(genre != null)
            {
                bool linkBook = await _orbookcontext.BookGenre.AnyAsync(bg => bg.Genre.Id == id);
                if(!linkBook)
                {
                    _orbookcontext.Remove(genre);
                    await _orbookcontext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _orbookcontext.Genre.ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            var genre = await _orbookcontext.Genre
                .FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return null;
            }

            return genre;
        }

        public async Task<List<Genre>?> GetList(int id)
        {
            var genres = await _orbookcontext.Book
                .Where(b => b.Id == id)
                .SelectMany(b => b.BookGenres)
                .Select(bg => bg.Genre)
                .ToListAsync();

            return genres;
        }

        public async Task<Genre?> UpdateAsync(Genre genre)
        {
            if(await _orbookcontext.Genre.AnyAsync(e => e.Id == genre.Id))
            {
                _orbookcontext.Genre.Update(genre);
                await _orbookcontext.SaveChangesAsync();
                return genre;
            }
            return null;
        }
    }
}
