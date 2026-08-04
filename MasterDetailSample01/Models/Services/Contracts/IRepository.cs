using MasterDetailSample01.ResponseFrameworks.Contracts;

namespace MasterDetailSample01.Models.Services.Contracts
{
    public interface IRepository<T> where T :  class
    {
        Task<IResponse<T>> InsertAsync(T obj);

        Task<IResponse<T>> UpdateAsync(T obj);

        Task<IResponse<T>> DeleteAsync(T obj);

        Task<IResponse<T>> selectAsync(T obj);

        Task<IResponse<IEnumerable<T>>> selectAllAsync();
    }
}
