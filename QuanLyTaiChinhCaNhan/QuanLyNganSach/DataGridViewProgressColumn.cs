using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNganSach
{

    public class DataGridViewProgressColumn : DataGridViewColumn
    {

        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
            this.ValueType = typeof(int);
        }
    }

    public class DataGridViewProgressCell : DataGridViewCell
    {

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            try
            {

                int progressVal = 0;
                if (value != null && value != DBNull.Value)
                {
                    if (value is decimal || value is double || value is float)
                    {
                        progressVal = (int)Math.Round(Convert.ToDouble(value));
                    }
                    else if (value is int)
                    {
                        progressVal = (int)value;
                    }
                }

                progressVal = Math.Max(0, Math.Min(100, progressVal));
                float percentage = progressVal / 100.0f;
                int progressWidth = (int)(cellBounds.Width * percentage);

                using (Brush backBrush = new SolidBrush(cellStyle.BackColor))
                {
                    graphics.FillRectangle(backBrush, cellBounds);
                }

                using (Brush progressBrush = new SolidBrush(Color.Green))
                {
                    graphics.FillRectangle(progressBrush, cellBounds.X, cellBounds.Y, progressWidth, cellBounds.Height);
                }

                using (Brush textBrush = new SolidBrush(cellStyle.ForeColor))
                {
                    string text = $"{progressVal}%";
                    SizeF textSize = graphics.MeasureString(text, cellStyle.Font);
                    graphics.DrawString(text, cellStyle.Font, textBrush,
                        cellBounds.X + (cellBounds.Width - textSize.Width) / 2,
                        cellBounds.Y + (cellBounds.Height - textSize.Height) / 2);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi vẽ ProgressBar: {ex.Message}");
            }
        }

        public override object Clone()
        {
            return base.Clone();
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }
            if (value is decimal || value is double || value is float)
            {
                return (int)Math.Round(Convert.ToDouble(value));
            }
            return value;
        }
    }
}