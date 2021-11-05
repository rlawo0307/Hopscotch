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
            public const int Board_Width = 500, Board_Height = 400;
            public const int Player_Width = 20, Player_Height = 20;
            //public const string ImagePath = "./res/Image/Image1.jpg";
        }

        class Player
        {
            public Point start_point;
            public Point cur_point;
            public int[,] board;

            public Player()
            {
                start_point = new Point(0, 0);
                cur_point = new Point(0,0);
                board = new int[Constants.Board_Height / Constants.Player_Height, Constants.Board_Width / Constants.Player_Width];
            }  
        }

        Player player;
        Button btn_player;
        Graphics g;
        SolidBrush sb;
        Panel board;
        Button[,] btn;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Form_Width, Constants.Form_Height);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //ImageBoard.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            //ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            //ImageBoard.Image = Image.FromFile(Constants.ImagePath);
            //g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            //g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 0, ImageBoard.Image.Width, ImageBoard.Image.Height));

            player = new Player();

            btn_player = new Button();
            btn_player.Size = new Size(Constants.Player_Width, Constants.Player_Height);
            btn_player.Location = player.cur_point;
            btn_player.BackColor = Color.White;
            btn_player.FlatAppearance.BorderSize = 0;
            btn_player.FlatStyle = FlatStyle.Flat;
            btn_player.Enabled = false;
            this.Controls.Add(btn_player);

            player.board[0, 0] = 1;

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
            
            btn = new Button[Constants.Board_Height / Constants.Player_Height, Constants.Board_Width / Constants.Player_Width];
            BtnAdd(0,0);
        }

        private void BtnAdd(int i, int j)
        {
            btn[i, j] = new Button();
            btn[i, j].Location = player.cur_point;
            btn[i, j].Size = new Size(Constants.Player_Width, Constants.Player_Height);
            btn[i, j].BackColor = Color.Red;
            btn[i, j].ForeColor = Color.Black;
            btn[i, j].Text = player.board[i, j].ToString();
            board.Controls.Add(btn[i,j]);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            Point prev_point = player.cur_point;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                switch (e.KeyCode)
                {
                    case Keys.A: if (player.cur_point.X > 0) player.cur_point.X -= Constants.Player_Width; break;
                    case Keys.D: if (player.cur_point.X + Constants.Player_Width < Constants.Board_Width) player.cur_point.X += Constants.Player_Width; break;
                    case Keys.S: if (player.cur_point.Y + Constants.Player_Height < Constants.Board_Height) player.cur_point.Y += Constants.Player_Height; break;
                    case Keys.W: if (player.cur_point.Y > 0) player.cur_point.Y -= Constants.Player_Height; break;
                }
                btn_player.Location = player.cur_point;
                g.FillRectangle(sb, new Rectangle(prev_point.X, prev_point.Y, Constants.Player_Width, Constants.Player_Height));
                //sb.Dispose();

                CheckBound(e.KeyCode);
            }
        }

        private void CheckBound(Keys key)
        {
            bool check = true;
            int i = player.cur_point.Y / Constants.Player_Height;
            int j = player.cur_point.X / Constants.Player_Width;
            
            if (player.board[i,j] == 1)
            {
                for (int k = i; k <= i; k++)
                    if (check)
                    {
                        for (int l = j; l < j; l++)
                            if (player.board[k,l] != 1)
                            {
                                check = false;
                                break;
                            }
                    }
            }
            else
            {
                player.board[i,j] = 1;
                BtnAdd(i,j);
            }
        }
    }

    
}
