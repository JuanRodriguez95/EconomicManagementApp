using System.Linq.Expressions;

namespace EconomicManagementAPP.Service
{
    public interface IGenericRepositorie<T> where T : class
    {
        Task Create(T entity); // Se agrega task por el asincronismo
        Task<IEnumerable<T>> ListData();
        Task<T> getById(int Id); // para el modify
        Task Delete(int Id);
        Task Modify(int Id, T entity);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
    }
}
