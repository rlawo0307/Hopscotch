using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopscotch
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            this.Size = new Size(600, 500);
            ImageBoard.Size = new Size(570, 450);
            ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            ImageBoard.Image = Image.FromFile("./res/Image/Image1.jpg");

            Graphics g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            DrawRec(g, 0,0, ImageBoard.Image.Width, ImageBoard.Height);

            //SolidBrush sb = new SolidBrush(Color.Red);
            //g.DrawEllipse(Pens.Red, -1, -1, 10, 10);

            Play();
            }

        private void Play()
        {
           
        }

        private void DrawRec(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush sb = new SolidBrush(Color.Gray);
            Rectangle rec = new Rectangle(x,y, width, height);

            g.FillRectangle(sb, rec);
            sb.Dispose();
        }
    }
}
