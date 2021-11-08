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

            public const int MyArea = 2;
        }

        class Player
        {
            public Button bp;
            public Point start;
            public Point end;
            public Point prev;
            public Point cur;

            public Player()
            {
                bp = new Button();
                bp.Size = new Size(Constants.Player_Width, Constants.Player_Height);
                bp.Location = cur;
                bp.BackColor = Color.White;
                bp.FlatAppearance.BorderSize = 0;
                bp.FlatStyle = FlatStyle.Flat;
                bp.Enabled = false;

                cur = new Point(0, 0);}  
        }

        class Board
        {
            public Panel panel_board;
            Graphics g;
            SolidBrush sb;
            public int[,] arr_board;

            public Board()
            {
                panel_board = new Panel();
                panel_board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
                panel_board.Location = new Point(0, 0);
                panel_board.BackColor = Color.Black;

                g = panel_board.CreateGraphics();
                sb = new SolidBrush(Color.Red);

                arr_board = new int[Constants.Board_Height, Constants.Board_Width];
            }

            public void DrawLine(Point point)
            {
                arr_board[point.Y, point.X] = 1;
                g.FillRectangle(sb, new Rectangle(point.X, point.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void DrawBoard()
            {

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
            //ImageBoard.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            //ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            //ImageBoard.Image = Image.FromFile(Constants.ImagePath);
            //g = Graphics.FromImage((System.Drawing.Image)ImageBoard.Image);
            //g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 0, ImageBoard.Image.Width, ImageBoard.Image.Height));

            player = new Player();
            this.Controls.Add(player.bp);

            board = new Board();
            this.Controls.Add(board.panel_board);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    board.arr_board[i, j] = Constants.MyArea; // area

            KeyPreview = true;
            this.KeyDown += Key_Down;

            //g.Clear(Color.Red);
        }

        /*
        private void BtnAdd(int i, int j, int val)
        {
            btn[i, j] = new Button();
            btn[i, j].Location = new Point(j*Constants.Player_Width, i*Constants.Player_Height);
            btn[i, j].Size = new Size(Constants.Player_Width, Constants.Player_Height);
            btn[i, j].FlatStyle = FlatStyle.Flat;
            btn[i, j].FlatAppearance.BorderSize = 0;
            btn[i, j].Text = val.ToString();
            if (val == 1)
                btn[i, j].BackColor = Color.Red;
            else if (val == Constants.itmp)
                btn[i, j].BackColor = Color.Gray;
            btn[i, j].ForeColor = Color.Black;
            board.Controls.Add(btn[i,j]);
        }
        */

        private void Key_Down(object sender, KeyEventArgs e)
        {
            player.prev = player.cur;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                switch (e.KeyCode)
                {
                    case Keys.A: if (player.cur.X > 0) player.cur.X -= Constants.Player_Width; break;
                    case Keys.D: if (player.cur.X + Constants.Player_Width < Constants.Board_Width) player.cur.X += Constants.Player_Width; break;
                    case Keys.S: if (player.cur.Y + Constants.Player_Height < Constants.Board_Height) player.cur.Y += Constants.Player_Height; break;
                    case Keys.W: if (player.cur.Y > 0) player.cur.Y -= Constants.Player_Height; break;
                }
                player.bp.Location = player.cur;
                board.DrawLine(player.prev);

                if (board.arr_board[player.prev.Y, player.prev.X] == Constants.MyArea && board.arr_board[player.cur.Y, player.cur.X] != Constants.MyArea)
                    player.start = player.prev;
                else if (board.arr_board[player.prev.Y, player.prev.X] != Constants.MyArea && board.arr_board[player.cur.Y, player.cur.X] == Constants.MyArea)
                {
                    player.end = player.cur;
                    board.DrawBoard();
                }
            }
        }
    }  
}
