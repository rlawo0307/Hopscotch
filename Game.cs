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
        Graphics g;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Form_Width, Constants.Form_Height);
            ImageBoard.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            ImageBoard.Image = Image.FromFile(Constants.ImagePath);

            g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            DrawRec(0, 0, ImageBoard.Image.Width, ImageBoard.Height, Color.Gray);

            Btn_Player.Size = new Size(Constants.Player_Width, Constants.Player_Height);
            Btn_Player.BackColor = Btn_Player.ForeColor = Btn_Player.FlatAppearance.BorderColor = Color.Red;
            player_x = ImageBoard.Location.X;
            player_y = ImageBoard.Location.Y;
            //PlayerMove();

            KeyPreview = true;
            this.KeyDown += Key_Down;

            //g.Clear(Color.Red);

            Play();
        }

        private void Play()
        {
            //PlayerMove();
            Btn_Player.Location = new Point(10+player_x,10+player_y);
            DrawRec(10, 10, 30, 30, Color.Blue);
            //DrawRec(player_x - ImageBoard.Location.X, player_y - ImageBoard.Location.Y, Constants.Player_Width, Constants.Player_Height, Color.Blue);

        }

        private void DrawRec(int x, int y, int width, int height, Color c)
        {
            SolidBrush sb = new SolidBrush(c);
            Rectangle rec = new Rectangle(x,y, width, height);

            g.FillRectangle(sb, rec);
            sb.Dispose();
        }

        private void PlayerMove()
        {
            Btn_Player.Location = new Point(player_x, player_y);
            //DrawRec(player_x - ImageBoard.Location.X, player_y- ImageBoard.Location.Y, Constants.Player_Width, Constants.Player_Height, Color.Blue);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: --player_x; break;
                case Keys.D: ++player_x; break;
                case Keys.S: ++player_y; break;
                case Keys.W: --player_y; break;
            }
            PlayerMove();
        }
    }

    static class Constants
    {
        public const int Form_Width = 600, Form_Height = 500;
        public const int Board_Width = 570, Board_Height = 450;
        public const int Player_Width = 30, Player_Height = 30;
        public const string ImagePath = "./res/Image/Image1.jpg";
    }
}
