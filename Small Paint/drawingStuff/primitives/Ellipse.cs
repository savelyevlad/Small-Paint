using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Paint.drawingStuff.primitives
{
    class Ellipse : Primitive
    {

        private int x1;
        private int y1;
        private int x2;
        private int y2;

        public int X1 { get => x1; set => x1 = value; }
        public int Y1 { get => y1; set => y1 = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }

        public Ellipse(Color color, int x1, int y1, int x2, int y2)
        {
            this.Color = color;

            this.X1 = x1;
            this.X2 = x2;
            this.Y1 = y1;
            this.Y2 = y2;

            draw();
        }

        public Ellipse(Color color, Point A, Point B) : this(color, A.X, A.Y, B.X, B.Y) { }

        public Ellipse(Color color, int x1, int y1, int x2, int y2, bool isFilled) : this(color, x1, y1, x2, y2)
        {
            this.IsFilled = isFilled;
            draw();
        }

        public Ellipse(Color color, Point A, Point B, bool isFilled) : this(color, A.X, A.Y, B.X, B.Y, isFilled) { }

        public override void draw()
        {
            if (Graphics == null)
            {
                throw new NullReferenceException("No Graphics instance");
            }
            else
            {
                if (IsFilled)
                {
                    Graphics.FillEllipse(Brush, Math.Min(X1, X2),
                                          Math.Min(Y1, Y2),
                                          Math.Abs(X2 - X1),
                                          Math.Abs(Y2 - Y1));
                }
                else
                {
                    Graphics.DrawEllipse(Pen, Math.Min(X1, X2),
                                        Math.Min(Y1, Y2),
                                        Math.Abs(X2 - X1),
                                        Math.Abs(Y2 - Y1));
                }
            }
        }
    }
}
