using PEA_WebAPI.Models;
using PEA_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace PEA_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _service;
        public ReportController(ReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Report>> GetReports()
        {
            return await _service.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Report>> GetReport(string id)
        {
            var report = await _service.GetAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Report report)
        {
            await _service.CreateAsync(report);
            return CreatedAtAction(nameof(GetReport), new {id = report.Id}, report);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Report updatedReport)
        {
            var report = await _service.GetAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            updatedReport.Id = report.Id;
            await _service.UpdateAsync(id, updatedReport);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var report = await _service.GetAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
