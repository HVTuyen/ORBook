using ORBook.Models;

namespace ORBook.Service.BookUsers
{
    public interface IBookUserService
    {
        Task<bool> Follow(int id, int bookId);
        Task<bool> UnFollow(int id, int bookId);
        Task<List<User>> GetBookUser(Volumn volumn);
    }
}
