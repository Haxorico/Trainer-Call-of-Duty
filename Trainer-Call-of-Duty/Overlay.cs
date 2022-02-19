using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trainer_Call_of_Duty.lib;
using Trainer_Call_of_Duty.lib.data;

namespace Trainer_Call_of_Duty
{
    public partial class Overlay : Form
    {
        IntPtr handle = User32.FindWindow(null, Offsets.NAME_WINDOW);
        bool windowActive { get; set; }
        bool isShowing { get; set; }
        List<Rectangle> emptyRects = new List<Rectangle>();
        List<Rectangle> fullRects = new List<Rectangle>();
        List<string> emptyRectText = new List<string>();
        List<Pen> emptyRectStyles = new List<Pen>();
        List<Color> fullRectStyles = new List<Color>();


        public void UpdateOverlayLocation()
        {
            //make the overlay the same size as the game window
            Rect gameRect;
            User32.GetWindowRect(handle, out gameRect);
            this.Size = new Size(gameRect.Right - gameRect.Left, gameRect.Bottom - gameRect.Top);

            //move the overlay to the game window
            this.Top = gameRect.Top;
            this.Left = gameRect.Left;

            windowActive = handle == User32.GetForegroundWindow();

            //refresh the overlay?
            if (windowActive)
            {
                if (isShowing)
                    this.Refresh();
                else
                {
                    isShowing = true;
                    this.Show();
                }
            }
            else
            {
                isShowing = false;
                this.Hide();
            }

        }

        public int AddRect(int x, int y, int width, int height, Color c, float thickness = 3, bool fill = false)
        {
            Rectangle r = new Rectangle(x, y, width, height);
            if (fill)
            {
                fullRects.Add(r);
                fullRectStyles.Add(c);
                return fullRects.Count;
            }
            else
            {
                emptyRectText.Add("");
                emptyRects.Add(r);
                emptyRectStyles.Add(new Pen(c, thickness));
                return emptyRects.Count;
            }

        }

        public void UpdateRect(int index, int x, int y, int width, int height, Color c, float thickness = -1, string msg = "")
        {
            if (thickness < 0)
                thickness = emptyRectStyles[index].Width;
            emptyRectText[index] = msg;
            emptyRects[index] = new Rectangle(x, y, width, height);
            emptyRectStyles[index].Color = c;
            emptyRectStyles[index].Width = thickness;
        }

        public void UpdateFullRect(int index, int x, int y, int width, int height, Color c)
        {
            fullRects[index] = new Rectangle(x, y, width, height);
            fullRectStyles[index] = c;
        }

        public Overlay()
        {
            InitializeComponent();
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            //make the overlay transparent
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;

            //make the overlay always on top
            this.TopMost = true;

            //remove the borders
            this.FormBorderStyle = FormBorderStyle.None;

            //make the overlay click-through
            uint initialStyle = User32.GetWindowLong(this.Handle, -20);
            User32.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            windowActive = false;
            isShowing = false;
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < emptyRects.Count; i++)
            {
                g.DrawRectangle(emptyRectStyles[i], emptyRects[i]);
                g.DrawString(emptyRectText[i], this.Font, new SolidBrush(Color.Yellow), (float)emptyRects[i].X, (float)emptyRects[i].Y);
            }
            for (int i = 0; i < fullRects.Count; i++)
            {
                SolidBrush sb = new SolidBrush(fullRectStyles[i]);
                g.FillRectangle(sb, fullRects[i]);
            }
        }
    }
}
