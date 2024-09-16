
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;

namespace ORBook.Service.BookUsers
{
    public class BookUserService : IBookUserService
    {
        private readonly ORBookContext _orbookcontext;
        public BookUserService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }
        public async Task<bool> Follow(int userId, int bookId)
        {
            var bookUser = new BookUser
            {
                BookId = bookId,
                UserId = userId
            };
            _orbookcontext.BookUser.Add(bookUser);

            await _orbookcontext.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetBookUser(Volumn volumn)
        {
            return await _orbookcontext.BookUser
                                .Where(bu => bu.BookId == volumn.BookId)
                                .Select(bu => bu.User)
                                .ToListAsync();
        }

        public async Task<bool> UnFollow(int userId, int bookId)
        {
            var bookUser = await _orbookcontext.BookUser
                                 .FirstOrDefaultAsync(bu => bu.UserId == userId && bu.BookId == bookId);
            if (bookUser == null)
            {
                return false;
            }
            _orbookcontext.BookUser.Remove(bookUser);
            await _orbookcontext.SaveChangesAsync();
            return true;

        }
    }
}
