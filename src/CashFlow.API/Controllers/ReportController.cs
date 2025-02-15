using System.Net.Mime;
using CashFlow.Application.UseCases.Reports.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromHeader] DateOnly month
    )
    {
        var file = await useCase.Execute(month);

        if (file.Length == 0) return NoContent();

        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }
}