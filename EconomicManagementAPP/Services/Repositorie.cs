using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EconomicManagementAPP.Services
{
    public class Repositorie<T> : IRepositorie<T> where T : class
    {
        private EconomicContext _context;

        public Repositorie(EconomicContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            
            T entity = await _context.Set<T>().FindAsync(Id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> getById(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<IEnumerable<T>> ListData()
        {
            return await _context.Set<T>().ToListAsync();
        }

    }
}
