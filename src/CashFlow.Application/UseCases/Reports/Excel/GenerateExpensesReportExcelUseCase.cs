using CashFlow.Domain.Entities;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(
    IReadOnlyExpensesRepository expensesRepository
) : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENT_SYMBOL = "$";

    public async Task<byte[]> Execute(DateOnly month)
    {
        var monthExpenses = await expensesRepository.GetByMonth(month);

        if (monthExpenses.Count == 0) return [];

        using var workbook = new XLWorkbook();

        workbook.Author = "Felipe Lira";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Calibri";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsertHeader(worksheet);
        InsertValues(worksheet, monthExpenses);
        // Adjust sizes
        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGeneratorMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGeneratorMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGeneratorMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGeneratorMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGeneratorMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.LightBlue;

        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("B1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("E1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
    }

    private void InsertValues(IXLWorksheet worksheet, List<Expense> expenses)
    {
        var row = 2;
        foreach (var expense in expenses)
        {
            worksheet.Cell(row, 1).Value = expense.Title;
            worksheet.Cell(row, 2).Value = expense.Date;
            worksheet.Cell(row, 3).Value = expense.PaymentType.ToString();
            worksheet.Cell(row, 4).Value = expense.Amount;
            worksheet.Cell(row, 4).Style.NumberFormat.Format = "-" + CURRENT_SYMBOL + "#,##0.00";
            worksheet.Cell(row, 5).Value = expense.Description;
            row++;
        }
    }
}