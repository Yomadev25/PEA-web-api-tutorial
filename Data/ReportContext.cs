using Microsoft.EntityFrameworkCore;
using PEA_WebAPI.Models;

namespace PEA_WebAPI.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) :base(options) { }
        public DbSet<Report> Reports { get; set; } = null!;
    }
}
