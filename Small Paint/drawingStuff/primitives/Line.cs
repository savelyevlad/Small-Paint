using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Paint.drawingStuff.primitives
{
    class Line : Primitive
    {
        private int x1;
        private int y1;
        private int x2;
        private int y2;

        public int X1 { get => x1; set => x1 = value; }
        public int Y1 { get => y1; set => y1 = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }

        public Line(Color color, int x1, int y1, int x2, int y2)
        {
            this.Color = color;

            this.X1 = x1;
            this.X2 = x2;
            this.Y1 = y1;
            this.Y2 = y2;

            draw();
        }

        public Line(Color color, Point A, Point B)
        {

            this.Color = color;

            this.X1 = A.X;
            this.Y1 = A.Y;

            this.X2 = B.X;
            this.Y2 = B.Y;

            draw();
        }

        public override void draw()
        {
            if (Graphics == null)
            {
                throw new NullReferenceException("No Graphics instance");
            }
            else
            {
                Graphics.DrawLine(Pen, X1, Y1, X2, Y2);
            }
        }
    }
}
