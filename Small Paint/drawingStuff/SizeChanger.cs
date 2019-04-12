using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Paint.drawingStuff
{

    // this class can help you to cut image
    // it crops left upper side

    public class SizeChanger
    {
        // no one can create an instance
        private SizeChanger() { }

        public static Bitmap changeSize(Bitmap bitmap, int width, int height)
        {
            // creating a new bitmap and drawing picture inside it
            Bitmap tempBitmap = new Bitmap(width, height);
            Graphics tempGraphics = Graphics.FromImage(tempBitmap);

            tempGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            tempGraphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            tempGraphics.Dispose();

            return tempBitmap;
        }
    }
}
