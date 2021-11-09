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
            public const int Player_Width = 10, Player_Height = 10;
            //public const string ImagePath = "./res/Image/Image1.jpg";

            public const int Empty = 0;
            public const int Path = 1;
            public const int Border = 2;
            public const int Area = 3;
            public const int ReversePoint = 4;

            public const int Forward = 1;
            public const int Reverse = -1;
        }

        class Player
        {
            public Button bp;
            public Point prev;
            public Point cur;
            public Point start;

            public Player()
            {
                bp = new Button();
                bp.Size = new Size(Constants.Player_Width, Constants.Player_Height);
                bp.Location = cur;
                bp.BackColor = Color.White;
                bp.FlatAppearance.BorderSize = 0;
                bp.FlatStyle = FlatStyle.Flat;
                bp.Enabled = false;

                cur = new Point(60, 60);
            }  
        }

        class Board
        {
            public Panel panel_board;
            public int[,] arr_board;
            Graphics g;
            Color[] color = { Color.Black, Color.Red, Color.Yellow, Color.Gray, Color.Blue };
            SolidBrush[] brush = new SolidBrush[5];
            public Board()
            {
                panel_board = new Panel();
                panel_board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
                panel_board.Location = new Point(0, 0);
                panel_board.BackColor = Color.Black;

                g = panel_board.CreateGraphics();

                arr_board = new int[Constants.Board_Height, Constants.Board_Width];

                for (int i = 0; i < 5; i++)
                    brush[i] = new SolidBrush(color[i]);
            }

            public void DrawPath(Point prev)
            {
                g.FillRectangle(brush[Constants.Path], new Rectangle(prev.X, prev.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void DrawBorder(Point prev)
            {
                g.FillRectangle(brush[Constants.Border], new Rectangle(prev.X, prev.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void DrawReversePoint(Point prev)
            {
                g.FillRectangle(brush[Constants.ReversePoint], new Rectangle(prev.X, prev.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void DrawArea(Point cur, int i,int j, int direc, int val)
            {
                if (i < 0 || i > Constants.Board_Height || j < 0 || j > Constants.Board_Width)
                    return;

                if (arr_board[i, j] == Constants.Path || arr_board[i,j] == Constants.Border) 
                    arr_board[i, j] = val;
                else
                    return;

                arr_board[i, j] = val;

                DrawBorder(new Point(j,i));
                MessageBox.Show("dd");

                if (arr_board[i - Constants.Player_Height, j] == Constants.Path)
                {
                    arr_board[i, j] = Constants.ReversePoint;
                    DrawReversePoint(new Point(j,i));
                }

                if (direc == Constants.Forward)
                    //|| (direc == Constants.Reverse && arr_board[i + Constants.Player_Height, j] == Constants.ReversePoint))
                    FillArea(i, j, direc);

                DrawArea(cur, i + Constants.Player_Height, j, Constants.Forward, Constants.Border);
                DrawArea(cur, i - Constants.Player_Height, j, Constants.Reverse, Constants.Border);
                DrawArea(cur, i, j - Constants.Player_Width, Constants.Forward, Constants.Border);
                DrawArea(cur, i, j + Constants.Player_Width, Constants.Forward, Constants.Border);
            }

            public void FillArea(int i, int x, int direc)
            {
                int j = x + Constants.Player_Width * direc;
                while (arr_board[i, j] == Constants.Empty)
                {
                    arr_board[i, j] = Constants.Area;
                    g.FillRectangle(brush[Constants.Area], new Rectangle(j, i, Constants.Player_Width, Constants.Player_Height));
                    j += Constants.Player_Width * direc;
                }
            }

        }

        Player player;
        Board board;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Form_Width, Constants.Form_Height);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            player = new Player();
            this.Controls.Add(player.bp);

            board = new Board();
            this.Controls.Add(board.panel_board);

            board.arr_board[player.cur.Y, player.cur.X] = Constants.Border;

            KeyPreview = true;
            this.KeyDown += Key_Down;
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
                    case Keys.W: if (player.cur.Y - Constants.Player_Height > 0) player.cur.Y -= Constants.Player_Height; break;
                }

                player.bp.Location = player.cur;

                if (player.cur.X <= player.start.X && player.cur.Y <= player.start.Y)
                    player.start = player.cur;

                if (board.arr_board[player.cur.Y, player.cur.X] == Constants.Empty)
                {
                    board.arr_board[player.cur.Y, player.cur.X] = Constants.Path;
                    //board.DrawPath(player.prev);
                }

                board.DrawPath(player.prev);
                //MessageBox.Show("cur : "+board.arr_board[player.cur.Y,player.cur.X]+"\nprev : "+ board.arr_board[player.prev.Y, player.prev.X]);

                if (board.arr_board[player.prev.Y, player.prev.X] == Constants.Border && board.arr_board[player.cur.Y, player.cur.X] == Constants.Path)
                {
                    MessageBox.Show("ss");
                    player.start = player.cur;
                }

                if (board.arr_board[player.prev.Y, player.prev.X] == Constants.Path && board.arr_board[player.cur.Y, player.cur.X] == Constants.Border)
                {
                    MessageBox.Show("dd");
                    board.DrawArea(player.cur, player.start.Y, player.start.X, Constants.Forward, Constants.Border);
                }
            }
        }
    }  
}
