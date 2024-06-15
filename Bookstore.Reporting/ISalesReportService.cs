using System.Globalization;
using Bookstore.Reporting.ReportEndpoints;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bookstore.Reporting;

internal interface ISalesReportService
{
    Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year);
}

internal class SalesReportService : ISalesReportService
{
    private readonly ILogger<SalesReportService> _logger;
    private readonly string _connString;

    public SalesReportService(IConfiguration config, ILogger<SalesReportService> logger)
    {
        _connString = config.GetConnectionString("ReportingConnectionString")!;
        _logger = logger;
    }

    public async Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year)
    {
        string sql = @"
SELECT BookId, Title, Author, UnitsSold as Units, TotalSales as Sales
FROM Reporting.MonthlyBookSales
WHERE Month = @month AND Year = @year
ORDER BY TotalSales DESC
";
        using var conn = new SqlConnection(_connString);
        _logger.LogInformation("Executing query {sql}", sql);

        var results = (await conn.QueryAsync<BookSalesResult>(sql, new { month, year })).ToList();

        var report = new TopBooksByMonthReport
        {
            Year = year,
            Month = month,
            MonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month),
            Results = results
        };

        return report;
    }
}
