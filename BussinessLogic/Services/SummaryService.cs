using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Shared.Models;

namespace BusinessLogic.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public SummaryService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SummaryModel>> GetAllAsync()
        {
            var summaries = await repository.SummaryRepository.GetAllAsync();
            var summaryModels = mapper.Map<IEnumerable<SummaryModel>>(summaries);

            return summaryModels;
        }

        public async Task<SummaryModel> GetByIdAsync(int id)
        {
            var summary = await repository.SummaryRepository.GetByIdAsync(id) 
                ?? throw new SummaryNotFoundException(id);
            var summaryModel = mapper.Map<SummaryModel>(summary);

            return summaryModel;
        }

        public async Task<SummaryModel> AddAsync(SummaryModel model)
        {
            var newSummary = mapper.Map<Summary>(model);
            await repository.SummaryRepository.AddAsync(newSummary);
            await repository.SaveAsync();
            var newSummaryModel = mapper.Map<SummaryModel>(newSummary);

            return newSummaryModel;
        }

        public async Task UpdateAsync(int id, SummaryModel model)
        {
            var summary = await repository.SummaryRepository.GetByIdAsync(id) 
                ?? throw new SummaryNotFoundException(model.Id);
            mapper.Map(model, summary);
            repository.SummaryRepository.Update(summary);
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            var summary = await repository.SummaryRepository.GetByIdAsync(modelId) 
                ?? throw new SummaryNotFoundException(modelId);
            repository.SummaryRepository.Delete(summary);
            await repository.SaveAsync();
        }
    }
}