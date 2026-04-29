using System;
using System.Collections.Generic;
using System.Windows;
using ClosedXML.Excel;
using REDConstruct.Models;

namespace REDConstruct.Services
{
    public class ExcelExportService
    {
        public static void ExportToExcel(List<RaportConsum> data, DateTime dataStart, DateTime dataEnd, string obiectiv)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Raport Consum");

                    // Header info
                    worksheet.Cell("A1").Value = "RAPORT CONSUM MATERIALE PE OBIECTIV";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A1").Style.Font.Size = 14;
                    worksheet.Range("A1:E1").Merge();

                    worksheet.Cell("A3").Value = $"Perioada: {dataStart:dd.MM.yyyy} - {dataEnd:dd.MM.yyyy}";
                    worksheet.Cell("A4").Value = $"Obiectiv: {(string.IsNullOrEmpty(obiectiv) || obiectiv == "Toate" ? "Toate" : obiectiv)}";

                    // Column headers
                    worksheet.Cell("A6").Value = "Obiectiv";
                    worksheet.Cell("B6").Value = "Material";
                    worksheet.Cell("C6").Value = "Cantitate";
                    worksheet.Cell("D6").Value = "Cost";
                    worksheet.Cell("E6").Value = "Data Calcul";

                    // Style headers
                    var headerRange = worksheet.Range("A6:E6");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Data rows
                    int row = 7;
                    decimal totalCost = 0;

                    foreach (var item in data)
                    {
                        worksheet.Cell(row, 1).Value = item.Obiectiv;
                        worksheet.Cell(row, 2).Value = item.Material;
                        worksheet.Cell(row, 3).Value = item.Cantitate;
                        worksheet.Cell(row, 4).Value = item.Cost;
                        worksheet.Cell(row, 4).Style.NumberFormat.Format = "_-* #,##0.00_-;-* #,##0.00_-;_-* \"-\"??_-;_-@_-";
                        worksheet.Cell(row, 5).Value = item.DataCalcul;
                        worksheet.Cell(row, 5).Style.DateFormat.Format = "dd.mm.yyyy";

                        totalCost += item.Cost;
                        row++;
                    }

                    // Total row
                    worksheet.Cell(row, 4).Value = totalCost;
                    worksheet.Cell(row, 4).Style.Font.Bold = true;
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "_-* #,##0.00_-;-* #,##0.00_-;_-* \"-\"??_-;_-@_-";
                    worksheet.Cell(row - 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(row - 1, 3).Value = "TOTAL:";
                    worksheet.Cell(row - 1, 3).Style.Font.Bold = true;

                    // Adjust column widths
                    worksheet.Columns("A", "E").AdjustToContents();

                    // Save file
                    string fileName = $"Raport_Consum_Materiale_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(downloadsPath, fileName);

                    workbook.SaveAs(filePath);
                    MessageBox.Show($"Raportul a fost exportat cu succes!\n\nFișier: {filePath}", "Export Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la exportul în Excel: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
