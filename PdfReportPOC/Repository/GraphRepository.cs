using Dapper;
using PdfReportPOC.Repository.DbModels;

namespace PdfReportPOC.Repository;

public interface IGraphRepository
{
    Task<IEnumerable<(string categories, decimal data)>> GetRevenueReportDataAsync();
    Task<IEnumerable<(string categories, int data)>> GetUserRoleCount();
    Task<IEnumerable<(string categories, int data)>> GetSummaryData();
}
public class GraphRepository : IGraphRepository
{
    private readonly DbContext _dbContext;
    public GraphRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<(string categories, decimal data)>> GetRevenueReportDataAsync()
    {
        string query = @"SELECT Dataname, Sum(DataValue) FROM GraphData 
                                WHERE GraphName = 'Revenue By Product'
                                GROUP By DataName";
        using (var connection = _dbContext.CreateConnection())
        {
            var data = await connection.QueryAsync<(string, decimal)>(query);
            return data;
        }
    }

    public async Task<IEnumerable<(string categories, int data)>> GetUserRoleCount()
    {
        string query = @"SELECT DataName, DataValue FROM GraphData
                                WHERE GraphName = 'User Role Count'";
        using(var connection = _dbContext.CreateConnection())
        {
            var data = await connection.QueryAsync<(string, int)>(query);
            return data;
        }
    }

    public async Task<IEnumerable<(string categories, int data)>> GetSummaryData()
    {
        string query = @"SELECT DataName, DataValueInt FROM EasyData";
        using(var connection = _dbContext.CreateConnection())
        {
            var data = await connection.QueryAsync<(string, int)>(query);
            return data;
        }
    }
    
}