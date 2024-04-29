namespace BusinessLogic.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task<TModel> AddAsync(TModel model);

        Task UpdateAsync(int id, TModel model);

        Task DeleteAsync(int modelId);
    }
}