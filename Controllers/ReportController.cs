using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEA_WebAPI.Data;
using PEA_WebAPI.Models;

namespace PEA_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportContext _context;
        public ReportController(ReportContext context)
        {
            _context = context;
        }

        [Route("get_report")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            if (_context.Reports == null) { return NotFound(); }
            return await _context.Reports.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            if (_context.Reports == null) { return NotFound(); }
            var showreport = await _context.Reports.FindAsync(id);
            if (showreport == null) { return NotFound(); }
            return showreport;
        }

        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            _context.Entry(report).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool ReportExist(long id)
        {
            return (_context.Reports?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (_context.Reports == null)
            {
                return NotFound();
            }
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }


}
