using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Models;

namespace ORBook.Service.Users
{
    public class UserService : IUserService, ICommonDataService<User>
    {
        private readonly ORBookContext _orbookcontext;
        public UserService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }

        public async Task<bool> CheckEmail(string email)
        {
            var user = await _orbookcontext.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<User> CreateAsync(User data)
        {
            _orbookcontext.User.Add(data);
            await _orbookcontext.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _orbookcontext.User.FindAsync(id);
            if (user != null)
            {
                _orbookcontext.Remove(user);
                await _orbookcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _orbookcontext.User.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _orbookcontext.User
                .FirstOrDefaultAsync(g => g.Id == id);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _orbookcontext.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> UpdateAsync(User data)
        {
            if (await _orbookcontext.User.AnyAsync(e => e.Id == data.Id))
            {
                if(data.role == null)
                {
                    data.role = "";
                }
                _orbookcontext.User.Update(data);
                await _orbookcontext.SaveChangesAsync();
                return data;
            }
            return null;
        }
    }
}
