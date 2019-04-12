using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Paint.drawingStuff.primitives
{
    class Rectangle : Primitive
    {

        private int x1;
        private int y1;
        private int x2;
        private int y2;

        public int X1 { get => x1; set => x1 = value; }
        public int Y1 { get => y1; set => y1 = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }

        public Rectangle(Color color, int x1, int y1, int x2, int y2)
        {
            this.Color = color;

            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;

            draw();
        }

        public Rectangle(Color color, int x1, int y1, int x2, int y2, bool isFilled) : this(color, x1, y1, x2, y2)
        {
            this.IsFilled = isFilled;
            draw();
        }

        public Rectangle(Color color, Point A, Point B) : this(color, A.X, A.Y, B.X, B.Y) { }

        public Rectangle(Color color, Point A, Point B, bool isFilled) : this(color, A.X, A.Y, B.X, B.Y, isFilled) { }

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
                    Graphics.FillRectangle(Brush, Math.Min(x1, x2),
                                            Math.Min(y1, y2),
                                            Math.Abs(x2 - x1),
                                            Math.Abs(y2 - y1));
                }
                else
                {
                    Graphics.DrawRectangle(Pen, Math.Min(x1, x2),
                                          Math.Min(y1, y2),
                                          Math.Abs(x2 - x1),
                                          Math.Abs(y2 - y1));
                }
            }
        }
    }
}
