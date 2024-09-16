using Humanizer.Localisation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ORBook.Data;
using ORBook.Hubs;
using ORBook.Models;

namespace ORBook.Service.Volumns
{
    public class VolumnService : ICommonDataService<Volumn>
    {
        private readonly ORBookContext _orbookcontext;
        public VolumnService(ORBookContext orbookcontext)
        {
            _orbookcontext = orbookcontext;
        }
        public async Task<Volumn> CreateAsync(Volumn volumn)
        {
            volumn.CreatedTime = DateTime.Now;
            volumn.UpdatedTime = DateTime.Now;
            _orbookcontext.Volumn.Add(volumn);
            await _orbookcontext.SaveChangesAsync();
            return volumn;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var volumn = await _orbookcontext.Volumn.FindAsync(id);
            if (volumn != null)
            {
                _orbookcontext.Remove(volumn);
                await _orbookcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Volumn>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Volumn> GetByIdAsync(int id)
        {
            var volumn = await _orbookcontext.Volumn
                .FirstOrDefaultAsync(g => g.Id == id);
            if (volumn == null)
            {
                return null;
            }
            return volumn;
        }

        public async Task<Volumn> UpdateAsync(Volumn volumn)
        {
            if (await _orbookcontext.Volumn.AnyAsync(e => e.Id == volumn.Id))
            {
                volumn.UpdatedTime = DateTime.Now;
                _orbookcontext.Volumn.Update(volumn);
                await _orbookcontext.SaveChangesAsync();
                return volumn;
            }
            return null;
        }
    }
}
