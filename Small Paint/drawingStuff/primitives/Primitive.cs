using System.Drawing;

namespace Small_Paint.drawingStuff.primitives
{
    // father of all primitives (line, ellipse, rectangle)
    public abstract class Primitive
    {
        private bool isFilled = false;
        private SolidBrush brush;
        private static Graphics graphics;
        private Pen pen;
        private Color color;

        public Color Color
        {
            set
            {
                color = value;
                Pen = new Pen(color);
                Brush = new SolidBrush(color);
            }
        }

        public static Graphics Graphics { get => graphics; set => graphics = value; }

        public Pen Pen { get => pen; set => pen = value; }

        public SolidBrush Brush { get => brush; set => brush = value; }
        
        
        public bool IsFilled { get => isFilled; set => isFilled = value; }

        // all primitives can be drawn
        public abstract void draw();
    }
}
