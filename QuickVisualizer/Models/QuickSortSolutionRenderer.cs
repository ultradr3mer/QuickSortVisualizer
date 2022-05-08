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
            int width = HorizontalResolution;
            int blockSize = HorizontalResolution / step.Arr.Length;
            int height = blockSize;

            var font = new Font("Cascadia Mono", 12);
            var fontBrush = new SolidBrush(Color.Black);
            var squareBrush = new SolidBrush(ColorTranslator.FromHtml("#76797C"));
            var pivotBrush = new SolidBrush(ColorTranslator.FromHtml("#0A92FE"));

            using (var bmp = new Bitmap(width, height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.Clear(Color.Pink);

                var len = step.Arr.Length;
                for (int i = 0; i < len; i++)
                { 
                    var item = step.Arr[i];
                    
                    var rect = new RectangleF(blockSize * i, 0, blockSize, blockSize);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    var backRect = rect;
                    backRect.Inflate(-3, -3);

                    g.FillRectangle(i == step.Pivot ? pivotBrush : squareBrush, backRect);
                    g.DrawString(item.ToString(), font, fontBrush, rect, format);
                }

                bmp.MakeTransparent(Color.Pink);
                byte[] byteArray = ToByteArray(bmp, ImageFormat.Png);

                return byteArray;
            }
        }
    }
}
