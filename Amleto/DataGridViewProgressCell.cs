using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Amleto
{
    class DataGridViewProgressCell : DataGridViewImageCell
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;

        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        
        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            try
            {
                int progressVal = (int)value;
                float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.

                // Draws the cell grid
                base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                // Draw the progress bar and the text
                g.FillRectangle(new SolidBrush(Color.FromArgb(8, 142, 6)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);

                string text = Value.ToString() + "%";
                Font f = cellStyle.Font;

                SizeF len = g.MeasureString(text, f);

                SolidBrush foreColor = new SolidBrush(cellStyle.ForeColor);

                if (DataGridView.Rows[rowIndex].Selected)
                    foreColor = new SolidBrush(cellStyle.SelectionForeColor);

                Point location = new Point(Convert.ToInt32(cellBounds.X + ((cellBounds.Width / 2) - len.Width / 2)), Convert.ToInt32(cellBounds.Y + ((cellBounds.Height / 2) - len.Height / 2)));
                g.DrawString(text, f, foreColor, location);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error painting cell");
            }
        }
    }
}
