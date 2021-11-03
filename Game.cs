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
        private int player_x;
        private int player_y;
        Button player;
        Graphics g;
        Pen p;
        Panel board;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Form_Width, Constants.Form_Height);
            //ImageBoard.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            //ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            //ImageBoard.Image = Image.FromFile(Constants.ImagePath);

            //g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            //g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 0, ImageBoard.Image.Width, ImageBoard.Image.Height));

            g = this.CreateGraphics();
            p = new Pen(Color.Red);
            p.Width = Constants.Player_Width;

            board = new Panel();
            board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            board.Location = new Point(0, 0);
            board.BackColor = Color.Black;
            this.Controls.Add(board);

            player_x = 50;
            player_y = 50;

            player = new Button();
            player.Size = new Size(Constants.Player_Width, Constants.Player_Height);
            player.Location = new Point(player_x, player_y);
            player.BackColor = Color.White;
            player.FlatAppearance.BorderSize = 0;
            player.FlatStyle = FlatStyle.Flat;
            player.Enabled = false;
            board.Controls.Add(player);

            KeyPreview = true;
            this.KeyDown += Key_Down;

            //g.Clear(Color.Red);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            int prev_x = player_x, prev_y = player_y;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                switch (e.KeyCode)
                {
                    case Keys.A: player_x-=5; break;
                    case Keys.D: player_x+=5; break;
                    case Keys.S: player_y+=5; break;
                    case Keys.W: player_y-=5; break;
                }
                g.DrawLine(p, prev_x, prev_y, player_x, player_y);
                player.Location = new Point(player_x, player_y);
            }
        }
    }

    static class Constants
    {
        public const int Form_Width = 600, Form_Height = 500;
        public const int Board_Width = 570, Board_Height = 450;
        public const int Player_Width = 10, Player_Height = 10;
        //public const string ImagePath = "./res/Image/Image1.jpg";
    }
}
