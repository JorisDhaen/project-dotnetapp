using BlazorDateRangePicker;
using Microsoft.AspNetCore.Components;
using Shared.Orders;
using System.Security.Claims;

namespace Client.Pages;
public partial class TablePage
{
    [Inject] public IOrderService? OrderService { get; set; }
    private string? OrderNr;
    private DateTimeOffset StartDate = DateTimeOffset.MinValue;
    private DateTimeOffset EndDate = DateTimeOffset.MaxValue;

    private IEnumerable<OrderDto.Index>? orders;
    private IEnumerable<OrderDto.Index>? ordersFiltered;
    private bool DateSorted = false;
    private bool StatusSorted = false;

    protected override async Task OnInitializedAsync()
    {
        await GetOrders();
        ordersFiltered = orders;
    }

    private async Task GetOrders()
    {
        OrderRequest.GetIndexUser request = new();
        Console.WriteLine(FakeAuthenticationProvider.Current.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        request.UserId = FakeAuthenticationProvider.Current.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await OrderService?.GetIndexUserAsync(request);
        orders = response.Orders;
    }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        OrderNr = args.Value?.ToString();
        FilterOrders();
    }

    private void OnRangeSelect(DateRange range)
    {
        //https://github.com/jdtcn/BlazorDateRangePicker
        StartDate = range.Start;
        EndDate = range.End;

        FilterOrders();
    }
    private void OnResetRange()
    {
        StartDate = DateTimeOffset.MinValue;
        EndDate = DateTimeOffset.MaxValue;
        FilterOrders();
    }

    private void FilterOrders()
    {
        if (!string.IsNullOrEmpty(OrderNr) && StartDate != DateTimeOffset.MinValue)
        {
            ordersFiltered = orders?.Where(x => x.OrderId?.Contains(OrderNr) == true).ToList();
            ordersFiltered = ordersFiltered?.Where(x => x.OrderDate >= StartDate && x.OrderDate <= EndDate).ToList();
        }else if(!string.IsNullOrEmpty(OrderNr) && StartDate == DateTimeOffset.MinValue)
        {
            ordersFiltered = orders?.Where(x => x.OrderId?.Contains(OrderNr) == true).ToList();
        }
        else if (string.IsNullOrEmpty(OrderNr) && StartDate != DateTimeOffset.MinValue)
        {
            ordersFiltered = orders?.Where(x => x.OrderDate >= StartDate && x.OrderDate <= EndDate).ToList();
        }
        else {
            ordersFiltered = orders;
        }
    }

    private void SortDate()
    {
        if (!DateSorted)
        {
            ordersFiltered = ordersFiltered?.OrderBy(x => x.OrderDate).ToList();
            DateSorted = true;
            StatusSorted = false;
        }
        else
        {
            ordersFiltered = ordersFiltered?.OrderByDescending(x => x.OrderDate).ToList();
            DateSorted = false;
            StatusSorted = false;
        }
    }

    private void SortStatus()
    {
        if (!StatusSorted)
        {
            ordersFiltered = ordersFiltered?.OrderBy(x => x.OrderStatus).ToList();
            StatusSorted = true;
            DateSorted = false;
        }
        else
        {
            ordersFiltered = ordersFiltered?.OrderByDescending(x => x.OrderStatus).ToList();
            StatusSorted = false;
            DateSorted = false;
        }
    }
}