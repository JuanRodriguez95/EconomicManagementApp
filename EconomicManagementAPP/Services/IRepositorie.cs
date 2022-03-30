namespace EconomicManagementAPP.Services
{
    public interface IRepositorie<T> where T : class
    {
        Task Create(T entity); // Se agrega task por el asincronismo
        Task<IEnumerable<T>> ListData();
        Task<T> getById(int Id); // para el modify
        Task Delete(int Id);
    }
}
