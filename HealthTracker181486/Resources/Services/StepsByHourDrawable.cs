using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;

namespace HealthTracker181486.Resources.Services
{
    public class StepsByHourDrawable : IDrawable
    {
        // 48 half-hour slots, 00:00 to 24:00. Fake data for the wireframe.
        public float[] Data { get; set; } = BuildSampleData();

        static float[] BuildSampleData()
        {
            var d = new float[48];
            d[2] = 15; d[3] = 15; d[4] = 20; d[5] = 15;                 // 01:00-02:30
            d[14] = 15; d[15] = 15; d[16] = 60; d[17] = 90; d[18] = 15;    // 07:00-09:00
            d[19] = 40; d[20] = 120; d[21] = 45; d[22] = 15;               // 09:30-11:00
            d[24] = 300; d[25] = 110;                                      // 12:00-12:30
            d[26] = 15; d[27] = 15; d[28] = 15; d[29] = 20;                // 13:00-14:30
            return d;
        }

        public void Draw(ICanvas canvas, RectF area)
        {
            const float axisSpace = 30f;              // room for tick labels under the line
            float left = 20, right = area.Width - 20;
            float baseline = area.Height - axisSpace;
            float plotWidth = right - left;
            float plotHeight = baseline - 8;

            float slotWidth = plotWidth / 48f;
            float barWidth = slotWidth * 0.5f;
            float max = Math.Max(1, Data.Max());

            // Bars
            canvas.FillColor = Color.FromArgb("#44D211");
            for (int i = 0; i < 48; i++)
            {
                if (Data[i] <= 0) continue;
                float h = Math.Max(barWidth, Data[i] / max * plotHeight);
                float x = left + i * slotWidth + (slotWidth - barWidth) / 2;
                canvas.FillRoundedRectangle(x, baseline - h, barWidth, h, barWidth / 2);
            }

            // Axis line
            canvas.StrokeColor = Color.FromArgb("#6B6B6B");
            canvas.StrokeSize = 2;
            canvas.DrawLine(left, baseline, right, baseline);

            // Ticks and labels at 0, 6, 12, 18, 24
            canvas.FontColor = Color.FromArgb("#B5B3B8");
            canvas.FontSize = 14;
            string[] labels = { "0", "6", "12", "18", "(h)" };
            for (int i = 0; i < labels.Length; i++)
            {
                float x = left + plotWidth * i / 4f;
                canvas.DrawLine(x, baseline, x, baseline + 6);
                canvas.DrawString(labels[i], x - 20, baseline + 8, 40, 20,
                    HorizontalAlignment.Center, VerticalAlignment.Top);
            }
        }
    }
}
