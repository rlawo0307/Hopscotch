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
    static class Constants
    {
        public const int Form_Width = 600, Form_Height = 500;
        public const int Board_Width = 570, Board_Height = 450;
        public const int Player_Width = 10, Player_Height = 10;
        public const string ImagePath = "./res/Image/Image1.jpg";
    }

    public partial class Game : Form
    {
        private int player_x;
        private int player_y;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Form_Width, Constants.Form_Height);
            ImageBoard.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            ImageBoard.Image = Image.FromFile(Constants.ImagePath);

            Graphics g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            DrawRec(g, 0, 0, ImageBoard.Image.Width, ImageBoard.Height);

            Btn_Player.Size = new Size(Constants.Player_Width, Constants.Player_Height);
            player_y = ImageBoard.Location.Y - Constants.Player_Height / 2;
            PlayerMove(player_x, player_y);
            Btn_Player.BackColor = Btn_Player.ForeColor = Btn_Player.FlatAppearance.BorderColor = Color.Red;

            KeyPreview = true;
            this.KeyDown += Key_Down;

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

        private void PlayerMove(int x, int y)
        {
            Btn_Player.Location = new Point(x, y);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: PlayerMove(--player_x, player_y); break;
                case Keys.D: PlayerMove(++player_x, player_y); break;
                case Keys.S: PlayerMove(player_x, ++player_y); break;
                case Keys.W: PlayerMove(player_x, --player_y); break;
            }
        }
    }
}
