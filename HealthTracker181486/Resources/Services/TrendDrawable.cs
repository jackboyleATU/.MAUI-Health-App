using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;

namespace HealthTracker181486.Resources.Services
{
    public class TrendDrawable : IDrawable
    {
        // One value per bar, oldest first, today last
        public int[] Data { get; set; } = { 4200, 6800, 3100, 8900, 5200, 7400, 5240 };

        // Changed per metric from the Trends page
        public Color BarColor { get; set; } = Color.FromArgb("#44D211");

        public void Draw(ICanvas canvas, RectF area)
        {
            int count = Data.Length;
            if (count == 0) return;

            const float labelSpace = 36f;
            float left = 16, right = area.Width - 16;
            float baseline = area.Height - labelSpace;
            float plotWidth = right - left;
            float plotHeight = baseline - 10;

            float slot = plotWidth / count;
            float barWidth = Math.Min(24f, slot * 0.4f);
            float max = Math.Max(1, Data.Max());

            for (int i = 0; i < count; i++)
            {
                float cx = left + slot * i + slot / 2;

                // Bar (shows as a dot when the value is small)
                if (Data[i] > 0)
                {
                    float h = Math.Max(barWidth, Data[i] / max * plotHeight);
                    canvas.FillColor = BarColor;
                    canvas.FillRoundedRectangle(cx - barWidth / 2, baseline - h, barWidth, h, barWidth / 2);
                }

                // Day-of-month label, today highlighted
                var date = DateTime.Today.AddDays(i - (count - 1));
                bool isToday = i == count - 1;
                canvas.FontColor = Color.FromArgb(isToday ? "#FF6B6B" : "#B5B3B8");
                canvas.FontSize = 16;
                canvas.DrawString(date.Day.ToString(), cx - 20, baseline + 10, 40, 24,
                    HorizontalAlignment.Center, VerticalAlignment.Top);
            }
        }
    }
}
