using ORBook.Models;

namespace ORBook.Service.Users
{
    public interface IUserService
    {
        Task<bool> CheckEmail(string email);
        Task<User> Login(string username, string password);
    }
}
