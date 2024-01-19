using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PEA_WebAPI.Models;

namespace PEA_WebAPI.Services
{
    public class ReportService
    {
        private readonly IMongoCollection<Report> _reportCollection;

        public ReportService(IOptions<ReportDatabaseSettings> reportDatabaseSetting)
        {
            var mongoClient = new MongoClient(
                reportDatabaseSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                reportDatabaseSetting.Value.DatabaseName);

            _reportCollection = mongoDatabase.GetCollection<Report>(
                reportDatabaseSetting.Value.CollectionName);
        }

        public async Task<List<Report>> GetAsync() =>
            await _reportCollection.Find(_ => true).ToListAsync();

        public async Task<Report?> GetAsync(string id) =>
            await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Report newReport) =>
            await _reportCollection.InsertOneAsync(newReport);

        public async Task UpdateAsync(string id, Report updatedReport) =>
            await _reportCollection.ReplaceOneAsync(x => x.Id == id, updatedReport);

        public async Task RemoveAsync(string id) =>
            await _reportCollection.DeleteOneAsync(x => x.Id == id);
    }
}
