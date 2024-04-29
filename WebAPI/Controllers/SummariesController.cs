using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        private readonly IServiceManager service;

        public SummariesController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SummaryModel>>> Get()
        {
            var summaries = await service.SummaryService.GetAllAsync();

            return Ok(summaries);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SummaryModel>> GetById(int id)
        {
            var summary = await service.SummaryService.GetByIdAsync(id);

            return Ok(summary);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SummaryModel summary)
        {
            var createdSummary = await service.SummaryService.AddAsync(summary);

            return CreatedAtAction(nameof(GetById), new { id = createdSummary.Id }, createdSummary);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SummaryModel summary)
        {
            await service.SummaryService.UpdateAsync(id, summary);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await service.SummaryService.DeleteAsync(id);

            return NoContent();
        }
    }
}