using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ScottPlot.WinForms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EduTrack
{
    public class ReportExporter
    {
        public static void ExportBothChartsToImages(
            FormsPlot barPlotControl,
            FormsPlot piePlotControl)
        {
            try
            {
                if (barPlotControl == null || piePlotControl == null)
                {
                    MessageBox.Show("The charts are not available.");
                    return;
                }

                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select a folder to save both charts as PNG images";

                    if (folderDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string folder = folderDialog.SelectedPath;

                    string barPath = Path.Combine(folder, "EduTrack_Program_Chart.png");
                    string piePath = Path.Combine(folder, "EduTrack_YearLevel_Chart.png");

                    barPlotControl.Plot.SavePng(barPath, 1000, 650);
                    piePlotControl.Plot.SavePng(piePath, 1000, 650);

                    ErrorHandler.ShowSuccess(
                        "Both charts saved successfully!\n\n" +
                        "1. " + barPath + "\n" +
                        "2. " + piePath);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "saving chart images");
            }
        }

        public static void ExportAnalyticsReport(
            DataTable studentData,
            int totalStudents,
            int bsitCount,
            int bscsCount,
            int otherCount,
            FormsPlot? programChart = null,
            FormsPlot? yearLevelChart = null)
        {
            string? programImagePath = null;
            string? yearImagePath = null;

            try
            {
                if (studentData == null || studentData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No student records available to include in the report.",
                        "Export Report",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PDF Report (*.pdf)|*.pdf";
                    saveDialog.Title = "Export Student Analytics Summary Report (PDF)";
                    saveDialog.FileName = "EduTrack_Analytics_Report.pdf";

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    // Set QuestPDF license and font settings.
                    QuestPDF.Settings.License = LicenseType.Community;
                    QuestPDF.Settings.UseSystemFonts = true;

                    byte[]? programImage = null;
                    byte[]? yearImage = null;

                    if (programChart != null)
                    {
                        programImagePath = Path.Combine(
                            Path.GetTempPath(),
                            Guid.NewGuid() + ".png");

                        programChart.Plot.SavePng(programImagePath, 900, 550);
                        programImage = File.ReadAllBytes(programImagePath);
                    }

                    if (yearLevelChart != null)
                    {
                        yearImagePath = Path.Combine(
                            Path.GetTempPath(),
                            Guid.NewGuid() + ".png");

                        yearLevelChart.Plot.SavePng(yearImagePath, 900, 550);
                        yearImage = File.ReadAllBytes(yearImagePath);
                    }

                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(25);
                            page.PageColor(Colors.White);
                            page.DefaultTextStyle(style => style.FontSize(10).FontColor("#0F172A"));

                            page.Header()
                                .PaddingBottom(12)
                                .Background("#0F172A")
                                .Padding(14)
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("EDUTRACK - STUDENT ANALYTICS & DIRECTORY REPORT")
                                        .FontSize(15)
                                        .Bold()
                                        .FontColor(Colors.White);

                                    column.Item()
                                        .PaddingTop(3)
                                        .Text("Generated on: " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"))
                                        .FontSize(9)
                                        .FontColor("#94A3B8");
                                });

                            page.Content()
                                .Column(column =>
                                {
                                    column.Spacing(14);

                                    column.Item().Row(row =>
                                    {
                                        row.Spacing(10);
                                        AddSummaryCard(row, "TOTAL STUDENTS", totalStudents.ToString(), "#4F46E5");
                                        AddSummaryCard(row, "BSIT ENROLLED", bsitCount.ToString(), "#10B981");
                                        AddSummaryCard(row, "BSCS ENROLLED", bscsCount.ToString(), "#F59E0B");
                                        AddSummaryCard(row, "OTHER PROGRAMS", otherCount.ToString(), "#8B5CF6");
                                    });

                                    if (programImage != null || yearImage != null)
                                    {
                                        column.Item().Row(row =>
                                        {
                                            row.Spacing(10);

                                            if (programImage != null)
                                            {
                                                row.RelativeItem()
                                                    .Border(1)
                                                    .BorderColor("#E2E8F0")
                                                    .Padding(6)
                                                    .Image(programImage)
                                                    .FitWidth();
                                            }

                                            if (yearImage != null)
                                            {
                                                row.RelativeItem()
                                                    .Border(1)
                                                    .BorderColor("#E2E8F0")
                                                    .Padding(6)
                                                    .Image(yearImage)
                                                    .FitWidth();
                                            }
                                        });
                                    }

                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1.4f);
                                            columns.RelativeColumn(2.4f);
                                            columns.RelativeColumn(1.0f);
                                            columns.RelativeColumn(1.2f);
                                            columns.RelativeColumn(1.2f);
                                            columns.RelativeColumn(1.8f);
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Element(HeaderCellStyle).Text("STUDENT ID");
                                            header.Cell().Element(HeaderCellStyle).Text("FULL NAME");
                                            header.Cell().Element(HeaderCellStyle).Text("GENDER");
                                            header.Cell().Element(HeaderCellStyle).Text("YEAR LEVEL");
                                            header.Cell().Element(HeaderCellStyle).Text("PROGRAM");
                                            header.Cell().Element(HeaderCellStyle).Text("CONTACT NUMBER");
                                        });

                                        for (int rowIndex = 0; rowIndex < studentData.Rows.Count; rowIndex++)
                                        {
                                            DataRow dataRow = studentData.Rows[rowIndex];
                                            string background = (rowIndex % 2 == 1) ? "#F8FAFC" : "#FFFFFF";

                                            string studentId = dataRow["student_id"]?.ToString() ?? "";
                                            string name = dataRow["name"]?.ToString() ?? "";
                                            string gender = dataRow["gender"]?.ToString() ?? "";
                                            string yearLevel = dataRow["year_level"]?.ToString() ?? "";
                                            string program = dataRow["program"]?.ToString() ?? "";
                                            string contact = dataRow["contact_number"]?.ToString() ?? "";

                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(studentId);
                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(name);
                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(gender);
                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(yearLevel);
                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(program);
                                            table.Cell().Element(cell => BodyCellStyle(cell, background)).Text(contact);
                                        }
                                    });
                                });

                            page.Footer()
                                .AlignCenter()
                                .Text(text =>
                                {
                                    text.DefaultTextStyle(style => style.FontSize(8).FontColor("#64748B"));
                                    text.Span("EduTrack Student Analytics Report | Page ");
                                    text.CurrentPageNumber();
                                    text.Span(" of ");
                                    text.TotalPages();
                                });
                        });
                    }).GeneratePdf(saveDialog.FileName);

                    ErrorHandler.ShowSuccess("PDF summary report exported successfully!");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, "exporting PDF summary report");
            }
            finally
            {
                if (programImagePath != null && File.Exists(programImagePath))
                {
                    File.Delete(programImagePath);
                }

                if (yearImagePath != null && File.Exists(yearImagePath))
                {
                    File.Delete(yearImagePath);
                }
            }
        }

        private static void AddSummaryCard(
            RowDescriptor row,
            string title,
            string value,
            string valueColorHex)
        {
            row.RelativeItem()
                .Border(1)
                .BorderColor("#E2E8F0")
                .Background("#F8FAFC")
                .Padding(10)
                .Column(column =>
                {
                    column.Item()
                        .Text(title)
                        .FontSize(8)
                        .Bold()
                        .FontColor("#64748B");

                    column.Item()
                        .PaddingTop(4)
                        .Text(value)
                        .FontSize(18)
                        .Bold()
                        .FontColor(valueColorHex);
                });
        }

        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background("#0F172A")
                .PaddingVertical(6)
                .PaddingHorizontal(6)
                .DefaultTextStyle(style => style.FontSize(8).Bold().FontColor(Colors.White));
        }

        private static IContainer BodyCellStyle(IContainer container, string backgroundColor)
        {
            return container
                .Background(backgroundColor)
                .BorderBottom(1)
                .BorderColor("#E2E8F0")
                .PaddingVertical(5)
                .PaddingHorizontal(6)
                .DefaultTextStyle(style => style.FontSize(8.5f).FontColor("#0F172A"));
        }
    }
}
