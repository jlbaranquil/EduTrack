
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ScottPlot;
using ScottPlot.WinForms;

namespace EduTrack
{
    public class StudentAnalytics
    {
        // Gets student enrollment grouped by academic program.
        public Dictionary<string, double> GetEnrollmentByProgram()
        {
            return GetEnrollmentCounts("program", "program enrollment");
        }

        // Gets student enrollment grouped by year level.
        public Dictionary<string, double> GetEnrollmentByYearLevel()
        {
            return GetEnrollmentCounts("year_level", "year-level enrollment");
        }

        // Gets student enrollment grouped by gender.
        public Dictionary<string, double> GetEnrollmentByGender()
        {
            return GetEnrollmentCounts("gender", "gender enrollment");
        }

        // Shared method for retrieving enrollment counts from MySQL.
        private Dictionary<string, double> GetEnrollmentCounts(
            string columnName, string action)
        {
            Dictionary<string, double> counts =
                new Dictionary<string, double>();

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
                            string category =
                                reader["category"].ToString() ?? "Unknown";

                            double total =
                                Convert.ToDouble(reader["total"]);

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

        // Renders student enrollment by program as a bar chart.
        public void RenderProgramBarChart(FormsPlot plotControl)
        {
            if (plotControl == null)
                return;

            Dictionary<string, double> data = GetEnrollmentByProgram();

            plotControl.Plot.Clear();

            if (data.Count == 0)
            {
                plotControl.Refresh();
                return;
            }

            string[] labels = new string[data.Count];
            ScottPlot.Bar[] bars = new ScottPlot.Bar[data.Count];

            int index = 0;

            foreach (var item in data)
            {
                labels[index] = item.Key;

                bars[index] = new ScottPlot.Bar
                {
                    Position = index,
                    Value = item.Value
                };

                index++;
            }

            plotControl.Plot.Add.Bars(bars);

            ScottPlot.Tick[] ticks = new ScottPlot.Tick[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                ticks[i] = new ScottPlot.Tick(i, labels[i]);
            }

            plotControl.Plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(ticks);

            plotControl.Plot.Title("Student Enrollment by Program");
            plotControl.Plot.XLabel("Program");
            plotControl.Plot.YLabel("Number of Students");

            plotControl.Plot.Axes.AutoScale();
            plotControl.Refresh();
        }

        // Renders student enrollment by year level as a donut/pie chart.
        public void RenderYearLevelPieChart(FormsPlot plotControl)
        {
            if (plotControl == null)
                return;

            Dictionary<string, double> data = GetEnrollmentByYearLevel();

            plotControl.Plot.Clear();

            if (data.Count == 0)
            {
                plotControl.Refresh();
                return;
            }

            List<ScottPlot.PieSlice> slices =
                new List<ScottPlot.PieSlice>();

            foreach (var item in data)
            {
                slices.Add(new ScottPlot.PieSlice
                {
                    Value = item.Value,
                    Label = item.Key
                });
            }

            var pie = plotControl.Plot.Add.Pie(slices);

            pie.DonutFraction = 0.45;

            plotControl.Plot.Title("Student Enrollment by Year Level");
            plotControl.Plot.ShowLegend();

            plotControl.Refresh();
        }
    }
}
