using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Shared.Models;

namespace BusinessLogic.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public RegionService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RegionModel>> GetAllAsync()
        {
            var regions = await repository.RegionRepository.GetAllAsync();
            var regionModels = mapper.Map<IEnumerable<RegionModel>>(regions);

            return regionModels;
        }

        public async Task<RegionModel> GetByIdAsync(int id)
        {
            var region = await repository.RegionRepository.GetByIdAsync(id) 
                ?? throw new RegionNotFoundException(id);
            var regionModel = mapper.Map<RegionModel>(region);

            return regionModel;
        }

        public async Task<RegionModel> AddAsync(RegionModel model)
        {
            var newRegion = mapper.Map<Region>(model);
            await repository.RegionRepository.AddAsync(newRegion);
            await repository.SaveAsync();
            var newRegionModel = mapper.Map<RegionModel>(newRegion);

            return newRegionModel;
        }

        public async Task UpdateAsync(int id, RegionModel model)
        {
            var region = await repository.RegionRepository.GetByIdAsync(id) 
                ?? throw new RegionNotFoundException(model.Id);
            mapper.Map(model, region);
            repository.RegionRepository.Update(region);
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            var region = await repository.RegionRepository.GetByIdAsync(modelId) 
                ?? throw new RegionNotFoundException(modelId);
            repository.RegionRepository.Delete(region);
            await repository.SaveAsync();
        }
    }
}