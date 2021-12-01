using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace OrderlyImagesDownloader
{
    public static class ImageHelper
    {
        /// <summary>
        /// 依据扩展名获取ImageFormat
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string extension)
        {
            extension = extension.TrimStart('.').ToLower();
            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "gif":
                    return ImageFormat.Gif;
                case "png":
                default:
                    return ImageFormat.Png;
            }
        }

        /// <summary>
        /// 获取图片字节流
        /// </summary>
        /// <param name="image"></param>
        /// <param name="extension">扩展名</param>
        /// <returns></returns>
        public static byte[] GetFormatBytes(Image image, string extension)
        {
            return GetFormatBytes(image, GetImageFormat(extension));
        }

        /// <summary>
        /// 获取图片字节流
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] GetFormatBytes(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// 画圆角矩形
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="rectangle"></param>
        /// <param name="radiusX"></param>
        /// <param name="radiusY"></param>
        public static void DrawRoundRectangle(this Graphics graphics, Pen pen, RectangleF rectangle, float radiusX, float radiusY = 0)
        {
            if (radiusY == 0)
            {
                radiusY = radiusX;
            }

            var mode = graphics.SmoothingMode;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            #region 方法1
            //float[] itXs = new float[3] { rectangle.X + radiusX, rectangle.X + rectangle.Width - 1 - radiusX, rectangle.X + rectangle.Width - 1 };
            //float[] itYs = new float[3] { rectangle.Y + radiusY, rectangle.Y + rectangle.Height - 1 - radiusY, rectangle.Y + rectangle.Height - 1 };
            ////上
            //graphics.DrawLine(pen, itXs[0], rectangle.Y, itXs[1], rectangle.Y);
            ////下
            //graphics.DrawLine(pen, itXs[0], itYs[2], itXs[1], itYs[2]);
            ////左
            //graphics.DrawLine(pen, rectangle.X, itYs[0], rectangle.X, itYs[1]);
            ////右
            //graphics.DrawLine(pen, itXs[2], itYs[0], itXs[2], itYs[1]);


            ////左上角
            //graphics.DrawArc(pen, rectangle.X, rectangle.Y, 2 * radiusX, 2 * radiusY, 180, 90);
            ////右上角
            //graphics.DrawArc(pen, itXs[1] - radiusX, rectangle.Y, 2 * radiusX, 2 * radiusY, 270, 90);
            ////左下角
            //graphics.DrawArc(pen, rectangle.X, itYs[1] - radiusY, 2 * radiusX, 2 * radiusY, 90, 90);
            ////右下角
            //graphics.DrawArc(pen, itXs[1] - radiusX, itYs[1] - radiusY, 2 * radiusX, 2 * radiusY, 0, 90);

            #endregion

            #region 方法2

            GraphicsPath GraphicsPath1 = new GraphicsPath();

            //x 坐标
            float[] itXs = new float[3]
            {
                rectangle.X + radiusX,
                rectangle.X + rectangle.Width - 1 - radiusX,
                rectangle.X + rectangle.Width - 1
            };
            //y 坐标
            float[] itYs = new float[3]
            {
                rectangle.Y + radiusY,
                rectangle.Y + rectangle.Height - 1 - radiusY,
                rectangle.Y + rectangle.Height - 1
            };

            //左边
            GraphicsPath1.AddLine(rectangle.X, itYs[1], rectangle.X, itYs[0]);
            //左上角
            GraphicsPath1.AddArc(rectangle.X, rectangle.Y, 2 * radiusX, 2 * radiusY, 180, 90);

            //上边
            GraphicsPath1.AddLine(itXs[0], rectangle.Y, itXs[1], rectangle.Y);
            //右上角
            GraphicsPath1.AddArc(itXs[1] - radiusX, rectangle.Y, 2 * radiusX, 2 * radiusY, -90, 90);

            //右边
            GraphicsPath1.AddLine(itXs[2], itYs[0], itXs[2], itYs[1]);
            //右下角
            GraphicsPath1.AddArc(itXs[1] - radiusX, itYs[1] - radiusY, 2 * radiusX, 2 * radiusY, 0, 90);

            //下边
            GraphicsPath1.AddLine(itXs[1], itYs[2], itXs[0], itYs[2]);
            //左下角
            GraphicsPath1.AddArc(rectangle.X, itYs[1] - radiusY, 2 * radiusX, 2 * radiusY, 90, 90);

            graphics.DrawPath(pen, GraphicsPath1);

            #endregion

            graphics.SmoothingMode = mode;
        }

        /// <summary>
        /// 获取网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImageByUrl(string url)
        {
            var tempUrl = String.Empty;
            if (url.Contains("http:") || url.Contains("https:"))
            {
                tempUrl = url;
            }
            else
            {
                tempUrl = "http:" + url;
            }
            var req = WebRequest.Create(tempUrl) as HttpWebRequest;
            req.Timeout = 100000;
            var ms = new MemoryStream();
            using (WebResponse resp = req.GetResponse())
            {
                var stream = resp.GetResponseStream();
                stream.CopyTo(ms);
            }
            Bitmap img = new Bitmap(ms);//你要的图像
            return img;
        }
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="imgToResize">源图片</param>
        /// <param name="size">新尺寸</param>
        /// <returns></returns>
        public static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        /// <summary>
        /// 切割图片为圆形
        /// </summary>
        /// <param name="imgSource"></param>
        /// <returns></returns>
        public static Bitmap MakeCicle(System.Drawing.Image imgSource)
        {
            Bitmap b = new Bitmap(imgSource.Width, imgSource.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath(System.Drawing.Drawing2D.FillMode.Alternate))
                {
                    p.AddEllipse(0, 0, imgSource.Width, imgSource.Height);
                    var ss = new TextureBrush(imgSource);
                    g.FillPath(ss, p);
                }
            }
            return b;
        }

        /// <summary>
        /// 获取圆角矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// 获取字体大小
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static float FontPixelsToPoints(int points)
        {
            return Convert.ToInt32((float)points / 96 * 72);
        }

        /// <summary>
        /// byte[] 转换 Bitmap
        /// </summary>
        /// <param name="Bytes">byte[]</param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }
        /// <summary>
        /// Bitmap转byte[]  
        /// </summary>
        /// <param name="Bitmap">Bitmap</param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        /// <summary>
        /// byte[]转换成Image
        /// </summary>
        /// <param name="byteArrayIn">二进制图片流</param>
        /// <returns>Image</returns>
        public static Image BytesToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }
        /// <summary>
        /// 图片路径转Byte
        /// </summary>
        /// <param name="imagePath">本地/相对图片路径</param>
        /// <returns></returns>
        public static byte[] ImgpathToBytes(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }
    }
}
