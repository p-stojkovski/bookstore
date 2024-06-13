using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Bookstore.Reporting.ReportEndpoints;

internal record TopSalesByMonthRequest(int Year, int Month);
internal record TopSalesByMonthResponse();

internal class TopSalesByMonth : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
    public override void Configure()
    {
        Get("/topsales");
        AllowAnonymous(); //TODO: lock down
    }

    public override async Task HandleAsync(TopSalesByMonthRequest request, 
        CancellationToken cancellationToken = default)
    {
        var response = new TopSalesByMonthResponse();

        await SendAsync(response);
    }
}
