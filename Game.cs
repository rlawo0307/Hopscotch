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
            public const int Form_Width = 600, Form_Height = 500;
            public const int Board_Width = 570, Board_Height = 450;
            public const int Player_Width = 10, Player_Height = 10;
            //public const string ImagePath = "./res/Image/Image1.jpg";
        }

        class Player
        {
            public int x;
            public int y;
        }

        Player player;
        Button btn_player;
        Graphics g;
        SolidBrush sb;
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

            player = new Player();
            player.x = 50;
            player.y = 50;

            btn_player = new Button();
            btn_player.Size = new Size(Constants.Player_Width, Constants.Player_Height);
            btn_player.Location = new Point(player.x, player.y);
            btn_player.BackColor = Color.White;
            btn_player.FlatAppearance.BorderSize = 0;
            btn_player.FlatStyle = FlatStyle.Flat;
            btn_player.Enabled = false;
            this.Controls.Add(btn_player);

            board = new Panel();
            board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            board.Location = new Point(0, 0);
            board.BackColor = Color.Black;
            this.Controls.Add(board);

            g = board.CreateGraphics();
            sb = new SolidBrush(Color.Red);

            KeyPreview = true;
            this.KeyDown += Key_Down;

            //g.Clear(Color.Red);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            
            int prev_x = player.x, prev_y = player.y;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                switch (e.KeyCode)
                {
                    case Keys.A: player.x -= 5; break;
                    case Keys.D: player.x += 5; break;
                    case Keys.S: player.y += 5; break;
                    case Keys.W: player.y -= 5; break;
                }
                btn_player.Location = new Point(player.x, player.y);
                g.FillRectangle(sb, new Rectangle(prev_x, prev_y, Constants.Player_Width, Constants.Player_Height));
                //sb.Dispose();
            }
        }
    }

    
}
