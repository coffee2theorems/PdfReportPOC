using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PdfReportPOC.Repository;

namespace PdfReportPOC.Pages;

public class Report : PageModel
{
    private readonly IGraphRepository _graphRepository;
    public Report(IGraphRepository graphRepository)
    {
        _graphRepository = graphRepository;
    }
    [BindProperty]
    public ReportModel SummaryData { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var dbData = await _graphRepository.GetSummaryData();
        SummaryData = new ReportModel()
        {
            DataDictionary = dbData.ToDictionary(u => u.categories, u => (u.categories, u.data))
        };
        return Page();
    }

    public async Task<IActionResult> OnGetRevenueReportAsync()
    {
        var data = await _graphRepository.GetRevenueReportDataAsync();
        var returnData = new
        {
            data = data.SelectMany(u => new[] { u.data }),
            categories = data.SelectMany(u => new[] { u.categories })
        };
        Console.WriteLine(JsonSerializer.Serialize(returnData));
        return new JsonResult(returnData);
    }
    
    public async Task<IActionResult> OnGetUserRolesReportAsync()
    {
        var data = await _graphRepository.GetUserRoleCount();
        var returnData = new
        {
            data = data.SelectMany(u => new[] { u.data }),
            categories = data.SelectMany(u => new[] { u.categories })
        };
        Console.WriteLine(JsonSerializer.Serialize(returnData));
        return new JsonResult(returnData);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // var dbData = await _graphRepository.GetSummaryData();
        // SummaryData = new ReportModel()
        // {
        //     DataDictionary = dbData.ToDictionary(u => u.categories, u => (u.categories, u.data))
        // };
        Console.WriteLine("Download");
        return Page();
    }
    
    public class ReportModel
    {
        public Dictionary<string, (string category, int data)> DataDictionary { get; set; }
    }
}