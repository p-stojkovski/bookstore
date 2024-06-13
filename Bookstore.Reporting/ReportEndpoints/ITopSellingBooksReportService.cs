using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bookstore.Reporting.ReportEndpoints;

internal interface ITopSellingBooksReportService
{
    TopBooksByMonthReport ReachInSqlQuery(int month, int year);
}

internal class TopSellingBooksReportService : ITopSellingBooksReportService
{
    private readonly string _connString;
    private readonly ILogger<TopSellingBooksReportService> _logger;

    public TopSellingBooksReportService(IConfiguration config,
        ILogger<TopSellingBooksReportService> logger)
    {
        _connString = config.GetConnectionString("OrderProcessingConnectionString")!;
        _logger = logger;
    }

    public TopBooksByMonthReport ReachInSqlQuery(int month, int year)
    {
        string sql = @"
SELECT B.Id, b.Title, b.Author , sum(oi.Quantity) as Units, sum(oi.UnitPrice * oi.Quantity) as Sales
FROM Books.Books b
	INNER JOIN OrderProcessing.OrderItem oi on b.Id = oi.BookId
	INNER JOIN OrderProcessing.Orders o on o.Id = oi.OrderId
WHERE MONTH(o.DateCreated) = 6 AND YEAR(o.DateCreated) = 2024
GROUP BY B.Id, b.Title, b.Author
ORDER BY Sales DESC";

        using var conn = new SqlConnection(_connString);

        _logger.LogInformation("Executing query: {sql}", sql);

        var results = conn.Query<BookSalesResult>(sql, new { month, year }).ToList();

        return new TopBooksByMonthReport
        {
            Year = year,
            Month = month,
            MonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month),
            Results = results
        }; ;
    }
}
