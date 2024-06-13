using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Reporting.ReportEndpoints;

internal class TopSalesByMonthRequest
{
    [FromQuery]
    public int Month { get; set; }

    [FromQuery]
    public int Year { get; set; }
};
internal class TopSalesByMonthResponse
{
    public TopBooksByMonthReport Report { get; set; } = default!;
}

internal class TopSalesByMonth : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
    private readonly ITopSellingBooksReportService _reportService;

    public TopSalesByMonth(ITopSellingBooksReportService _reportService)
    {
        this._reportService = _reportService;
    }

    public override void Configure()
    {
        Get("/topsales");
        AllowAnonymous(); //TODO: lock down
    }

    public override async Task HandleAsync(TopSalesByMonthRequest request,
        CancellationToken cancellationToken = default)
    {
        var report = _reportService.ReachInSqlQuery(request.Month, request.Year);

        var response = new TopSalesByMonthResponse { Report = report };

        await SendAsync(response);
    }
}
