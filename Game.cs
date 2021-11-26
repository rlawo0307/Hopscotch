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
        static class Constants
        {
            public const int Board_Width = 600, Board_Height = 500;
            public const int Player_Width = 10, Player_Height = 10;
            //public const string ImagePath = "./res/Image/Image1.jpg";
        }

        class Player
        {
            public Button bp;
            public Point cur;
            public Point prev;

            public Player()
            {
                prev = cur = new Point(0, 0);

                bp = new Button();
                bp.Size = new Size(Constants.Player_Width, Constants.Player_Height);
                bp.Location = cur;
                bp.BackColor = Color.White;
                bp.FlatAppearance.BorderSize = 0;
                bp.FlatStyle = FlatStyle.Flat;
                bp.Enabled = false;
            }  
        }

        class Board
        {
            public Panel panel_board;
            Point pos;

            Graphics g;
            Color[] color = { Color.Black, Color.Red, Color.Gray }; // Empty, Path, Area
            SolidBrush[] brush = new SolidBrush[3];
            public Board(Point p)
            {
                panel_board = new Panel();
                panel_board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
                panel_board.Location = new Point(0, 0);
                panel_board.BackColor = Color.Black;
                pos = p;

                g = panel_board.CreateGraphics();

                for (int i = 0; i < 3; i++)
                    brush[i] = new SolidBrush(color[i]);
            }

            public Color GetColor(Point p)
            {
                Bitmap bmp = new Bitmap(Constants.Player_Width, Constants.Player_Height);
                Color c;
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.CopyFromScreen(pos.X + p.X, pos.Y + p.Y, 0, 0, new Size(Constants.Player_Width, Constants.Player_Height));
                }
                bmp.Save("./res/player.jpg");
                c = bmp.GetPixel(0, 0);
                bmp.Dispose();
                return c;
            }

            public void DrawPath(Point p)
            {
                g.FillRectangle(brush[1], new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void FillArea(int i, int j, int direc)
            {
                g.FillRectangle(brush[2], new Rectangle(j, i, Constants.Player_Width, Constants.Player_Height));
            }
        }

        Player player;
        Board board;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Board_Width+16, Constants.Board_Height+39);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0,0);

            player = new Player();
            this.Controls.Add(player.bp);

            board = new Board(new Point(this.Left+12, this.Top+this.Height-this.ClientRectangle.Height+8));
            this.Controls.Add(board.panel_board);

            KeyPreview = true;
            this.KeyDown += Key_Down;

            this.Show();
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                player.prev = player.cur;
                switch (e.KeyCode)
                {
                    case Keys.A: if (player.cur.X > 0) player.cur.X -= Constants.Player_Width; break;
                    case Keys.D: if (player.cur.X + Constants.Player_Width < Constants.Board_Width) player.cur.X += Constants.Player_Width; break;
                    case Keys.S: if (player.cur.Y + Constants.Player_Height < Constants.Board_Height) player.cur.Y += Constants.Player_Height; break;
                    case Keys.W: if (player.cur.Y > 0) player.cur.Y -= Constants.Player_Height; break;
                }
                player.bp.Location = player.cur;
                board.DrawPath(player.prev);

                Color c = board.GetColor(player.prev);
                MessageBox.Show("" + c);
                c = board.GetColor(player.cur);
                MessageBox.Show("" + c);
            }
        }
    }  
}
