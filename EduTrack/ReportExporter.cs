
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
        // Exports both dashboard charts as PNG images.
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

                using (FolderBrowserDialog folderDialog =
                    new FolderBrowserDialog())
                {
                    folderDialog.Description =
                        "Select a folder to save the chart images.";

                    if (folderDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string folder = folderDialog.SelectedPath;

                    string barPath = Path.Combine(
                        folder, "ProgramEnrollment.png");

                    string piePath = Path.Combine(
                        folder, "YearLevelEnrollment.png");

                    barPlotControl.Plot.SavePng(barPath, 1200, 800);
                    piePlotControl.Plot.SavePng(piePath, 1200, 800);

                    ErrorHandler.ShowSuccess(
                        "Both chart images were exported successfully.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(
                    ex, "export chart images");
            }
        }

        // Generates a PDF containing enrollment summaries,
        // dashboard charts, and the student directory.
        public static void ExportAnalyticsReport(
            DataTable studentData,
            int totalStudents,
            int bsitCount,
            int bscsCount,
            int otherCount,
            FormsPlot? programChart,
            FormsPlot? yearLevelChart)
        {
            string? programImagePath = null;
            string? yearImagePath = null;

            try
            {
                if (studentData == null)
                {
                    MessageBox.Show("Student data is not available.");
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveDialog.Title = "Save Analytics Report";
                    saveDialog.FileName = "EduTrack_Analytics_Report.pdf";

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    // Set QuestPDF license.
                    QuestPDF.Settings.License = LicenseType.Community;

                    byte[]? programImage = null;
                    byte[]? yearImage = null;

                    // Save charts temporarily so they can be embedded in the PDF.
                    if (programChart != null)
                    {
                        programImagePath = Path.Combine(
                            Path.GetTempPath(),
                            Guid.NewGuid() + ".png");

                        programChart.Plot.SavePng(
                            programImagePath, 1200, 800);

                        programImage = File.ReadAllBytes(programImagePath);
                    }

                    if (yearLevelChart != null)
                    {
                        yearImagePath = Path.Combine(
                            Path.GetTempPath(),
                            Guid.NewGuid() + ".png");

                        yearLevelChart.Plot.SavePng(
                            yearImagePath, 1200, 800);

                        yearImage = File.ReadAllBytes(yearImagePath);
                    }

                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4.Landscape());
                            page.Margin(30);
                            page.DefaultTextStyle(
                                style => style.FontSize(9));

                            page.Header()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("EduTrack")
                                        .FontSize(22)
                                        .Bold()
                                        .FontColor("#17365D");

                                    column.Item()
                                        .Text("Analytics & Student Directory Summary Report")
                                        .FontSize(14)
                                        .SemiBold();

                                    column.Item()
                                        .Text("Generated: " +
                                            DateTime.Now.ToString("MMMM dd, yyyy"))
                                        .FontSize(9)
                                        .FontColor("#666666");
                                });

                            page.Content()
                                .PaddingVertical(15)
                                .Column(column =>
                                {
                                    // Enrollment summary cards.
                                    column.Item().Text("Enrollment Overview")
                                        .FontSize(15).Bold();

                                    column.Item().PaddingTop(10).Row(row =>
                                    {
                                        AddSummaryCard(
                                            row, "Total Students",
                                            totalStudents.ToString(), "#17365D");

                                        AddSummaryCard(
                                            row, "BSIT Students",
                                            bsitCount.ToString(), "#198754");

                                        AddSummaryCard(
                                            row, "BSCS Students",
                                            bscsCount.ToString(), "#0D6EFD");

                                        AddSummaryCard(
                                            row, "Other Programs",
                                            otherCount.ToString(), "#D97706");
                                    });

                                    // Chart section.
                                    column.Item().PaddingTop(20)
                                        .Text("Enrollment Charts")
                                        .FontSize(15).Bold();

                                    column.Item().PaddingTop(10).Row(row =>
                                    {
                                        if (programImage != null)
                                        {
                                            row.RelativeItem().Column(chart =>
                                            {
                                                chart.Item().Text(
                                                    "Enrollment by Program")
                                                    .Bold();

                                                chart.Item().Image(programImage)
                                                    .FitArea();
                                            });
                                        }

                                        if (yearImage != null)
                                        {
                                            row.RelativeItem().Column(chart =>
                                            {
                                                chart.Item().Text(
                                                    "Enrollment by Year Level")
                                                    .Bold();

                                                chart.Item().Image(yearImage)
                                                    .FitArea();
                                            });
                                        }
                                    });

                                    // Student directory section.
                                    column.Item().PageBreak();

                                    column.Item().Text("Student Directory")
                                        .FontSize(15).Bold();

                                    column.Item().PaddingTop(10).Table(table =>
                                    {
                                        int columnCount = studentData.Columns.Count;

                                        table.ColumnsDefinition(columns =>
                                        {
                                            for (int i = 0; i < columnCount; i++)
                                            {
                                                columns.RelativeColumn();
                                            }
                                        });

                                        // Table headings.
                                        table.Header(header =>
                                        {
                                            foreach (DataColumn col in studentData.Columns)
                                            {
                                                header.Cell()
                                                    .Element(HeaderCellStyle)
                                                    .Text(col.ColumnName);
                                            }
                                        });

                                        // Student records.
                                        for (int rowIndex = 0;
                                             rowIndex < studentData.Rows.Count;
                                             rowIndex++)
                                        {
                                            DataRow dataRow =
                                                studentData.Rows[rowIndex];

                                            string background =
                                                rowIndex % 2 == 0
                                                ? "#F2F5F9"
                                                : "#FFFFFF";

                                            foreach (DataColumn col in studentData.Columns)
                                            {
                                                string value =
                                                    dataRow[col] == DBNull.Value
                                                    ? ""
                                                    : dataRow[col].ToString() ?? "";

                                                table.Cell()
                                                    .Element(cell =>
                                                        BodyCellStyle(cell, background))
                                                    .Text(value);
                                            }
                                        }
                                    });
                                });

                            page.Footer()
                                .AlignCenter()
                                .Text(text =>
                                {
                                    text.Span("EduTrack | Page ");
                                    text.CurrentPageNumber();
                                    text.Span(" of ");
                                    text.TotalPages();
                                });
                        });
                    }).GeneratePdf(saveDialog.FileName);

                    ErrorHandler.ShowSuccess(
                        "Analytics PDF report exported successfully.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(
                    ex, "export analytics report");
            }
            finally
            {
                // Remove temporary chart images.
                if (programImagePath != null &&
                    File.Exists(programImagePath))
                {
                    File.Delete(programImagePath);
                }

                if (yearImagePath != null &&
                    File.Exists(yearImagePath))
                {
                    File.Delete(yearImagePath);
                }
            }
        }

        // Creates a summary card in the PDF.
        private static void AddSummaryCard(
            RowDescriptor row,
            string title,
            string value,
            string valueColorHex)
        {
            row.RelativeItem()
                .Padding(5)
                .Border(1)
                .BorderColor("#D9E2F3")
                .Background("#F8FAFC")
                .Padding(12)
                .Column(column =>
                {
                    column.Item()
                        .Text(title)
                        .FontSize(9)
                        .FontColor("#555555");

                    column.Item()
                        .PaddingTop(5)
                        .Text(value)
                        .FontSize(20)
                        .Bold()
                        .FontColor(valueColorHex);
                });
        }

        // Styles the PDF table header.
        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background("#17365D")
                .Padding(6)
                .DefaultTextStyle(style =>
                    style.FontColor("#FFFFFF").Bold());
        }

        // Styles the PDF table body cells.
        private static IContainer BodyCellStyle(
            IContainer container,
            string backgroundColor)
        {
            return container
                .Background(backgroundColor)
                .BorderBottom(1)
                .BorderColor("#D9E2F3")
                .Padding(5);
        }
    }
}
