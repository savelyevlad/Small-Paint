using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Small_Paint.drawingStuff
{
    // changes background color of picturebox on selected one
    class ColorChanger
    {
        // no one can create an instance
        private ColorChanger() { }

        public static void change(ColorDialog colorDialog, PictureBox pictureBox, ref Color color)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.BackColor = colorDialog.Color;
                color = pictureBox.BackColor;
            }
        }
    }
}
