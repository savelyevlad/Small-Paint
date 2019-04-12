using Small_Paint.drawingStuff;
using Small_Paint.drawingStuff.primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Small_Paint
{
    public partial class MainForm : Form
    {
        
        private static MainForm instance = new MainForm();
        private Thread thread;
        private SelectedTool selectedTool = SelectedTool.Clear;

        private List<Primitive> primitives = new List<Primitive>();

        private Graphics graphics;
        private Point startPoint;
        private Bitmap bitmap;
        private Color color = Color.Black;
        private bool isClicked = false;

        private bool isCtrlPressed = false;

        // for ctrl + s hotkey
        private bool notSaved = true;

        public static MainForm Instance { get => instance; }

        // private constuctor so no one can create instance of form (except static one inside it)
        private MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        // button help
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are helpless :(\nby the way ctrl+z works");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // creating bitmap with size of screen
            bitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 20, 
                                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 20);

            // creating graphics instance on which we will draw
            graphics = Graphics.FromImage(bitmap);

            // creating thread, that will change picturebox size, while form size is changing
            thread = new Thread(() =>
            {
                while (true)
                {
                    // analogue runOnUiThread() in java
                    Invoke((MethodInvoker)delegate
                    {
                        if (groupBox.Size.Width != Width - 40)
                        {
                            groupBox.Size = new Size(Width - 40, groupBox.Height);
                            pictureBox.Size = new Size(Width - 40, pictureBox.Height);
                        }
                        if (pictureBox.Size.Height != Height - 146)
                        {
                            pictureBox.Size = new Size(pictureBox.Width, Height - 146);
                        }
                    });
                }
            });
            thread.Start();

            Primitive.Graphics = graphics;

            // Important thing. Ctrl+z will not work without it
            KeyPreview = true;
        }
        
        // when form closes
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // we should abort thread because of possibilities of exeptions
            thread.Abort();
        }

        // button line
        private void btnLine_Click(object sender, EventArgs e)
        {
            // selection "line" tool
            selectedTool = SelectedTool.Line;

            btnLine.Enabled = false;
            btnCircle.Enabled = true;
            btnRectange.Enabled = true;
        }

        // button rectangle
        private void btnRectange_Click(object sender, EventArgs e)
        {
            // selection "rectangle" tool
            selectedTool = SelectedTool.Rectangle;

            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectange.Enabled = false;
        }

        // button circle
        private void btnCircle_Click(object sender, EventArgs e)
        {
            // selection "circle" tool
            selectedTool = SelectedTool.Circle;

            btnLine.Enabled = true;
            btnCircle.Enabled = false;
            btnRectange.Enabled = true;
        }

        // button clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (primitives.Count > 0 && notSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Maybe want to save you masterpiece?",
                                                        "Save or not?", MessageBoxButtons.YesNo);

                if (dialogResult.Equals(DialogResult.Yes))
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }

            // unselection
            selectedTool = SelectedTool.Clear;
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectange.Enabled = true;

            // clearing image
            graphics.Clear(Color.White);
            primitives.Clear();
            pictureBox.Image = bitmap;
        }

        // when left mouse button gets clicked
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!selectedTool.Equals(SelectedTool.Clear))
            {
                // memorizing start point
                startPoint = e.Location;
                isClicked = true;
            }
        }

        // when left mouse button gets unclicked
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
            // draw primitive and add to primitives list
            switch (selectedTool)
            {
                case SelectedTool.Line:
                    primitives.Add(new Line(color, startPoint, e.Location));
                    break;
                case SelectedTool.Rectangle:
                    drawingStuff.primitives.Rectangle rectangle = new drawingStuff.primitives.Rectangle(color, startPoint, e.Location, checkIsFilled.Checked);
                    rectangle.IsFilled = checkIsFilled.Checked;
                    primitives.Add(rectangle);
                    break;
                case SelectedTool.Circle:
                    drawingStuff.primitives.Ellipse ellipse = new drawingStuff.primitives.Ellipse(color, startPoint, e.Location, checkIsFilled.Checked);
                    ellipse.IsFilled = checkIsFilled.Checked;
                    primitives.Add(ellipse);
                    break;
            }
            pictureBox.Image = bitmap;
        }

        // changes color of primitives
        private void pctCurrentColor_Click(object sender, EventArgs e) 
        {
            ColorChanger.change(colorDialog, pctCurrentColor, ref color);
        }

        // draws all stuff while left mouse button is clicked and mouse is moving above
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                notSaved = true;
                graphics.Clear(Color.White);
                drawAll();
                switch (selectedTool)
                {
                    case SelectedTool.Line:
                        Line line = new Line(color, startPoint, e.Location);
                        break;
                    case SelectedTool.Rectangle:
                        drawingStuff.primitives.Rectangle rectangle = new drawingStuff.primitives.Rectangle(color, startPoint, e.Location, checkIsFilled.Checked);
                        rectangle.IsFilled = checkIsFilled.Checked;
                        break;
                    case SelectedTool.Circle:
                        drawingStuff.primitives.Ellipse ellipse = new drawingStuff.primitives.Ellipse(color, startPoint, e.Location, checkIsFilled.Checked);
                        ellipse.IsFilled = checkIsFilled.Checked;
                        break;
                }
                pictureBox.Image = bitmap;
            }
        }

        // draw all primitives in primitives list
        private void drawAll() 
        {
            foreach (var item in primitives)
            {
                item.draw();
            }
        }

        // if "save" button was clicked
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            saveFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

            notSaved = false;
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveImage(SizeChanger.changeSize(bitmap, pictureBox.Width, pictureBox.Height));
            }
        }

        private void saveImage(Bitmap bitmap)
        {
            Stream fs = saveFileDialog.OpenFile();
            bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        // button exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (primitives.Count > 0 && notSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Maybe want to save you masterpiece?",
                                                        "Save or not?", MessageBoxButtons.YesNo);

                if (dialogResult.Equals(DialogResult.Yes))
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }

            Application.Exit();
        }

        // hotkeys handling
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if ctrl pressed
            if (isCtrlPressed)
            {
                // if z pressed
                if (e.KeyChar == 26)
                {
                    // delete last drawn element
                    if (primitives.Count > 0)
                    {
                        primitives.RemoveAt(primitives.Count - 1);
                        graphics.Clear(Color.White);
                        drawAll();
                        pictureBox.Image = bitmap;
                    }
                }
                // if s pressed
                if (e.KeyChar == 19)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
        }

        // hotkeys handling
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            isCtrlPressed = e.Control; // ctrl was pressed
        }

        // hotkeys handling
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            isCtrlPressed = false; // ctrl was unpressed
        }
    }
}
