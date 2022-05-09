using QuickVisualizer.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#pragma warning disable CA1416 // Validate platform compatibility

namespace QuickVisualizer.Models
{
    public class QuickSortSolutionRenderer
    {
        public static byte[] ToByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        private const int HorizontalResolution = 1000;

        internal static byte[] Render(QuickSortSolutionStep step)
        {
            if (step.Arr.Length == 0)
            {
                return new byte[] { };
            }

            int width = HorizontalResolution;
            int blockSize = HorizontalResolution / step.Arr.Length;
            int height = blockSize;

            var font = new Font("Cascadia Mono", 12);
            var fontBrush = new SolidBrush(Color.Black);
            var squareBrush = new SolidBrush(ColorTranslator.FromHtml("#76797C"));
            var pivotBrush = new SolidBrush(ColorTranslator.FromHtml("#0A92FE"));
            var backColor = ColorTranslator.FromHtml("#A4A9AD");
            var disabledBrush = new SolidBrush(ColorTranslator.FromHtml("#55A4A9AD"));

            using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backColor);


                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;


                var len = step.Arr.Length;
                for (int i = 0; i < len; i++)
                {
                    var item = step.Arr[i];

                    var rect = new RectangleF(blockSize * i, 0, blockSize, blockSize);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    var backRect = rect;
                    backRect.Inflate(-1.5f, -1.5f);
                    var rounded = GetRoundedRect(backRect, 6.0f);

                    g.FillPath(i == step.PivotIndex ? pivotBrush : squareBrush, rounded);
                    g.DrawString(item.ToString(), font, fontBrush, rect, format);
                }

                if (step.Switch != null)
                {
                    using (Pen p = new Pen(Color.Black, 2.0f))
                    using (AdjustableArrowCap cap = new AdjustableArrowCap(5,5))
                    {
                        p.CustomEndCap = cap;
                        p.CustomStartCap = cap;

                        g.DrawLine(p, blockSize * ((float)step.Switch.Left + 0.5f) + 10, height - 10.5f, blockSize * ((float)step.Switch.Right + 0.5f) - 10, height - 10.5f);
                    }
                }

                for (int i = 0; i < len; i++)
                {
                    if (i >= step.Left && i <= step.Right)
                    {
                        continue;
                    }

                    var item = step.Arr[i];
                    var rect = new Rectangle(blockSize * i, 0, blockSize, blockSize);
                    g.FillRectangle(disabledBrush, rect);
                }

                byte[] byteArray = ToByteArray(bmp, ImageFormat.Png);

                return byteArray;
            }
        }

        private static GraphicsPath GetRoundedRect(RectangleF baseRect,
            float radius)
        {
            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
