using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.ResponseFrameworks.Contracts;

namespace MasterDetailSample01.ApplicationServices.services.Contracts
{
    public interface IApplicationService<TPost, TPut, TDelete, TGet, TGetAll> 
    {
        Task<IResponse<TPost>> PostAsync(TPost obj);
        Task<IResponse<TPut>> PutAsync(TPut obj);
        Task <IResponse<TDelete>> DeleteAsync(TDelete obj);
        Task <IResponse<TGet>> GetAsync(TGet obj);
        //Task<IResponse<TGet>> GetViewAsync(TView obj);
        Task<IResponse<IEnumerable<TGetAll>>> GetAllAsync();
    }
}
