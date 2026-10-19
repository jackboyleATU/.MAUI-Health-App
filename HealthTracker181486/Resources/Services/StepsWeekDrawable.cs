using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker181486.Resources.Services
{
    public class StepsWeekDrawable : IDrawable
    {
        // Oldest day first, today last. Fake data for the wireframe.
        public int[] Data { get; set; } = { 4200, 6800, 3100, 8900, 5200, 300, 7240 };

        // Dotted average line from the original. Off by default since you're skipping that part.
        public bool ShowAverageLine { get; set; } = false;

        public void Draw(ICanvas canvas, RectF area)
        {
            const int days = 7;
            const float labelSpace = 36f;

            float left = 16, right = area.Width - 16;
            float baseline = area.Height - labelSpace;
            float plotWidth = right - left;
            float plotHeight = baseline - 10;

            float slot = plotWidth / days;
            float barWidth = Math.Min(24f, slot * 0.4f);
            float max = Math.Max(1, Data.Max());

            // Optional dotted line at the average
            if (ShowAverageLine)
            {
                float avgY = baseline - (float)Data.Average() / max * plotHeight;
                canvas.StrokeColor = Color.FromArgb("#6B6B6B");
                canvas.StrokeSize = 2;
                canvas.StrokeLineCap = LineCap.Round;
                canvas.StrokeDashPattern = new float[] { 0.1f, 4f };
                canvas.DrawLine(left, avgY, right, avgY);
                canvas.StrokeDashPattern = null;
            }

            for (int i = 0; i < days; i++)
            {
                float cx = left + slot * i + slot / 2;

                // Bar (becomes a dot when the value is small)
                if (Data[i] > 0)
                {
                    float h = Math.Max(barWidth, Data[i] / max * plotHeight);
                    canvas.FillColor = Color.FromArgb("#44D211");
                    canvas.FillRoundedRectangle(cx - barWidth / 2, baseline - h, barWidth, h, barWidth / 2);
                }

                // Day-of-month label, today highlighted
                var date = DateTime.Today.AddDays(i - (days - 1));
                bool isToday = i == days - 1;
                canvas.FontColor = Color.FromArgb(isToday ? "#FF6B6B" : "#B5B3B8");
                canvas.FontSize = 16;
                canvas.DrawString(date.Day.ToString(), cx - 20, baseline + 10, 40, 24,
                    HorizontalAlignment.Center, VerticalAlignment.Top);
            }
        }
    }
}
