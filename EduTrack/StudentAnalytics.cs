using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ScottPlot;
using ScottPlot.WinForms;

namespace EduTrack
{
    public class StudentAnalytics
    {
        public Dictionary<string, double> GetEnrollmentByProgram()
        {
            return GetEnrollmentCounts("program", "program enrollment");
        }

        public Dictionary<string, double> GetEnrollmentByYearLevel()
        {
            return GetEnrollmentCounts("year_level", "year-level enrollment");
        }

        public Dictionary<string, double> GetEnrollmentByGender()
        {
            return GetEnrollmentCounts("gender", "gender enrollment");
        }

        private Dictionary<string, double> GetEnrollmentCounts(string columnName, string action)
        {
            Dictionary<string, double> counts = new Dictionary<string, double>();

            try
            {
                using (MySqlConnection conn = StudentDatabase.GetConnection())
                {
                    conn.Open();

                    string query =
                        $"SELECT COALESCE(NULLIF(TRIM({columnName}), ''), 'Unknown') AS category, " +
                        "COUNT(*) AS total " +
                        $"FROM students GROUP BY category ORDER BY category;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string category = reader["category"].ToString() ?? "Unknown";
                            double total = Convert.ToDouble(reader["total"]);
                            counts[category] = total;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowDatabaseError(ex, action);
            }

            return counts;
        }

        public void RenderProgramBarChart(FormsPlot plotControl)
        {
            if (plotControl == null)
                return;

            plotControl.Plot.Clear();
            plotControl.Plot.Axes.Rules.Clear();

            Dictionary<string, double> data = GetEnrollmentByProgram();

            if (data.Count == 0)
            {
                plotControl.Plot.Title("Enrollment by Program (No Data)");
                plotControl.Plot.Axes.AutoScale();
                plotControl.Refresh();
                return;
            }

            string[] hexColors = new string[] { "#4F46E5", "#10B981", "#F59E0B", "#8B5CF6", "#EC4899", "#06B6D4" };
            string[] labels = new string[data.Count];
            ScottPlot.Bar[] bars = new ScottPlot.Bar[data.Count];

            int index = 0;

            foreach (var item in data)
            {
                labels[index] = item.Key;
                string colorHex = hexColors[index % hexColors.Length];

                bars[index] = new ScottPlot.Bar
                {
                    Position = index,
                    Value = item.Value,
                    FillColor = ScottPlot.Color.FromHex(colorHex),
                    Label = item.Value.ToString("0")
                };

                index++;
            }

            var barPlot = plotControl.Plot.Add.Bars(bars);
            barPlot.ValueLabelStyle.Bold = true;
            barPlot.ValueLabelStyle.FontSize = 12;

            ScottPlot.Tick[] ticks = new ScottPlot.Tick[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                ticks[i] = new ScottPlot.Tick(i, labels[i]);
            }

            plotControl.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            plotControl.Plot.Axes.Margins(bottom: 0, top: 0.25);

            plotControl.Plot.Title("Student Enrollment by Program");
            plotControl.Plot.XLabel("Academic Program");
            plotControl.Plot.YLabel("Number of Students");

            plotControl.Plot.Axes.AutoScale();
            plotControl.Refresh();
        }

        public void RenderYearLevelPieChart(FormsPlot plotControl)
        {
            if (plotControl == null)
                return;

            plotControl.Plot.Clear();
            plotControl.Plot.Axes.Rules.Clear();

            Dictionary<string, double> data = GetEnrollmentByYearLevel();

            if (data.Count == 0)
            {
                plotControl.Plot.Title("Students by Year Level (No Data)");
                plotControl.Plot.Axes.AutoScale();
                plotControl.Refresh();
                return;
            }

            string[] hexColors = new string[] { "#4F46E5", "#10B981", "#F59E0B", "#8B5CF6", "#EF4444" };
            List<ScottPlot.PieSlice> slices = new List<ScottPlot.PieSlice>();
            int colorIndex = 0;

            foreach (var item in data)
            {
                slices.Add(new ScottPlot.PieSlice
                {
                    Value = item.Value,
                    Label = item.Key + " (" + item.Value.ToString("0") + ")",
                    LegendText = item.Key + " - " + item.Value.ToString("0"),
                    FillColor = ScottPlot.Color.FromHex(hexColors[colorIndex % hexColors.Length])
                });

                colorIndex++;
            }

            var pie = plotControl.Plot.Add.Pie(slices);
            pie.DonutFraction = 0.45;
            pie.ManageAxisLimits = false;

            plotControl.Plot.ShowLegend();
            plotControl.Plot.Axes.Frameless();
            plotControl.Plot.HideGrid();
            plotControl.Plot.Title("Student Distribution by Year Level");

            plotControl.Plot.Axes.AutoScale();

            if (plotControl.Width >= plotControl.Height)
            {
                var squareRule = new ScottPlot.AxisRules.SquarePreserveY(
                    plotControl.Plot.Axes.Bottom,
                    plotControl.Plot.Axes.Left);
                plotControl.Plot.Axes.Rules.Add(squareRule);
            }
            else
            {
                var squareRule = new ScottPlot.AxisRules.SquarePreserveX(
                    plotControl.Plot.Axes.Bottom,
                    plotControl.Plot.Axes.Left);
                plotControl.Plot.Axes.Rules.Add(squareRule);
            }

            plotControl.Refresh();
        }
    }
}
