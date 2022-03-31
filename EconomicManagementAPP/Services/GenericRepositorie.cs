using EconomicManagementAPP.Data;
using EconomicManagementAPP.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace EconomicManagementAPP.Services
{
    public class GenericRepositorie<T> : IGenericRepositorie<T> where T : class
    {
        private EconomicContext _context;

        public GenericRepositorie(EconomicContext context)
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
            var data = await _context.Set<T>().ToListAsync();
            int i = 0;
            return data;
        }

        public async Task Modify(int Id,T entity)
        {
            T localEntity = await _context.Set<T>().FindAsync(Id);
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Object value = property.GetValue(entity);
                property.SetValue(localEntity, value);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> expression)
        {
            var result = await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
