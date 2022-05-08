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
            if(step.Arr.Length == 0)
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

            using (var bmp = new Bitmap(width, height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.Clear(backColor);

                var len = step.Arr.Length;
                for (int i = 0; i < len; i++)
                { 
                    var item = step.Arr[i];
                    
                    var rect = new Rectangle(blockSize * i, 0, blockSize, blockSize);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    var backRect = rect;
                    backRect.Inflate(-3, -3);

                    g.FillRectangle(i == step.PivotIndex ? pivotBrush : squareBrush, backRect);
                    g.DrawString(item.ToString(), font, fontBrush, rect, format);
                }

                if (step.Switch != null)
                {
                    using (Pen p = new Pen(Color.Black,2))
                    using (GraphicsPath capPath = new GraphicsPath())
                    {
                        int capsize = 5;

                        // A triangle
                        capPath.AddLine(-capsize, 0, capsize, 0);
                        capPath.AddLine(-capsize, 0, 0, capsize);
                        capPath.AddLine(0, capsize, capsize, 0);

                        p.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);
                        p.CustomStartCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);

                        g.DrawLine(p, blockSize * ((float)step.Switch.Left + 0.5f), height - 10, blockSize * ((float)step.Switch.Right + 0.5f), height-10);
                    }
                }


                    //bmp.MakeTransparent(Color.Pink);
                byte[] byteArray = ToByteArray(bmp, ImageFormat.Png);

                return byteArray;
            }
        }
    }
}
