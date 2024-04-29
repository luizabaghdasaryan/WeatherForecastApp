using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IServiceManager service;

        public RegionsController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionModel>>> Get()
        {
            var regions = await service.RegionService.GetAllAsync();

            return Ok(regions);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RegionModel>> GetById(int id)
        {
            var region = await service.RegionService.GetByIdAsync(id);

            return Ok(region);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegionModel region)
        {
            var createdRegion = await service.RegionService.AddAsync(region);

            return CreatedAtAction(nameof(GetById), new { id = createdRegion.Id }, createdRegion);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] RegionModel region)
        {
            await service.RegionService.UpdateAsync(id, region);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await service.RegionService.DeleteAsync(id);

            return NoContent();
        }
    }
}