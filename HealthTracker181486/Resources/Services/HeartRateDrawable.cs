using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;

namespace HealthTracker181486.Resources.Services
{
    public class HeartRateDrawable : IDrawable
    {
        const float LabelColumn = 44f;   // space on the right for 50/100/150/200
        const float PlotInset = 12f;
        const float TopMargin = 46f;     // room above the 200 line for the callout bubble
        const float BottomSpace = 34f;   // room below the baseline for hour labels
        const float MaxBpm = 200f;

        // 48 half-hour slots. A Max of 0 means no reading. Fake data for the wireframe.
        public float[] Min { get; set; } = new float[48];
        public float[] Max { get; set; } = new float[48];
        public int SelectedSlot { get; set; } = 19;   // slot shown in the bubble; -1 hides it

        public HeartRateDrawable()
        {
            Set(4, 68, 84); Set(5, 60, 76); Set(6, 56, 74); Set(7, 52, 78);
            Set(8, 50, 72); Set(9, 53, 70); Set(10, 60, 66); Set(11, 50, 74);
            Set(12, 54, 82); Set(13, 50, 68); Set(14, 52, 70); Set(15, 50, 66);
            Set(17, 60, 100); Set(18, 62, 94); Set(19, 66, 96);
        }

        void Set(int slot, float min, float max) { Min[slot] = min; Max[slot] = max; }

        public int SlotFromX(float x, float width)
        {
            float slotW = (width - LabelColumn - PlotInset) / 48f;
            return Math.Clamp((int)((x - PlotInset) / slotW), 0, 47);
        }

        public void Draw(ICanvas canvas, RectF area)
        {
            float width = area.Width, height = area.Height;
            float baseline = height - BottomSpace;
            float plotLeft = PlotInset, plotRight = width - LabelColumn;
            float hourW = (plotRight - plotLeft) / 24f;
            float slotW = hourW / 2f;

            float Y(float bpm) => baseline - bpm / MaxBpm * (baseline - TopMargin);
            float X(int slot) => plotLeft + slot * slotW + slotW / 2f;

            // Gridlines with bpm labels under each line, right-aligned
            canvas.StrokeColor = Color.FromArgb("#3A3A3C");
            canvas.StrokeSize = 2;
            canvas.FontColor = Color.FromArgb("#B5B3B8");
            canvas.FontSize = 14;
            for (int v = 0; v <= 200; v += 50)
            {
                float y = Y(v);
                canvas.DrawLine(0, y, width, y);
                if (v > 0)
                    canvas.DrawString(v.ToString(), width - LabelColumn, y + 2, LabelColumn, 18,
                        HorizontalAlignment.Right, VerticalAlignment.Top);
            }

            // Hour ticks and labels at 0, 6, 12, 18
            canvas.StrokeColor = Color.FromArgb("#6B6B6B");
            for (int h = 0; h < 24; h += 6)
            {
                float x = plotLeft + h * hourW;
                canvas.DrawLine(x, baseline, x, baseline + 8);
                canvas.DrawString(h.ToString(), x - 20, baseline + 10, 40, 20,
                    HorizontalAlignment.Center, VerticalAlignment.Top);
            }

            // Range bars: min to max for each slot; the selected one is lighter
            float barW = slotW * 0.7f;
            for (int i = 0; i < 48; i++)
            {
                if (Max[i] <= 0) continue;
                float top = Y(Max[i]);
                float bottom = Y(Min[i]);
                canvas.FillColor = Color.FromArgb(i == SelectedSlot ? "#FF9DBF" : "#FF4D8D");
                canvas.FillRoundedRectangle(X(i) - barW / 2, top, barW,
                    Math.Max(barW, bottom - top), 2);
            }

            
        }
    }
}
