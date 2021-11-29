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

            public const int Empty = 0;
            public const int Path = 1;
            public const int Area = 2;
        }

        class Player
        {
            public Point cur;
            public Point prev;
            public Point left_top;
            public Point right_bottom;
            public bool start;
            
            public Player(Point p)
            {
                prev = cur = left_top = p;
            }
            
            public void UpdateLeftTop()
            {
                if (cur.X <= left_top.X && cur.Y <= left_top.Y)
                    left_top = cur;
            }

            public void UpdateRightBottom()
            {
                if (cur.X >= right_bottom.X && cur.Y >= right_bottom.Y)
                    right_bottom = cur;
            }
        }

        class Board
        {
            public Panel panel_board;
            Graphics g;
            Color[] color = { Color.Black, Color.Red, Color.Gray, Color.White }; // Empty, Path, Area, Player
            SolidBrush[] brush = new SolidBrush[4];
            int[,] board;
            int board_row, board_col;

            public Board(Point p)
            {
                board_row = Constants.Board_Height / Constants.Player_Height;
                board_col = Constants.Board_Width / Constants.Player_Width;
                panel_board = new Panel();
                panel_board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
                panel_board.Location = new Point(0, 0);
                panel_board.BackColor = Color.Black;

                board = new int[board_row, board_col];
                for (int i = 0; i < board_row; i++)
                    for (int j = 0; j < board_col; j++)
                        board[i, j] = Constants.Empty;

                g = panel_board.CreateGraphics();

                for (int i = 0; i < 4; i++)
                    brush[i] = new SolidBrush(color[i]);
            }

            public void DarwBorder()
            {
                Point tmp = new Point(0, 0);
                for (int i = 0; i < board_col; i++)
                {
                    tmp.X = i * Constants.Player_Width;

                    tmp.Y = 0;
                    DrawRect(tmp, Constants.Area);
                    tmp.Y = Constants.Board_Height - Constants.Player_Height;
                    DrawRect(tmp, Constants.Area);
                }
                for (int i = 0; i < board_row; i++)
                {
                    tmp.Y = i * Constants.Player_Height;

                    tmp.X = 0;
                    DrawRect(tmp, Constants.Area);
                    tmp.X = Constants.Board_Width - Constants.Player_Width;
                    DrawRect(tmp, Constants.Area);
                }

            }

            public void DrawRect(Point p, int val)
            {
                int i = p.Y / Constants.Player_Height;
                int j = p.X / Constants.Player_Width;

                if (board[i, j] != Constants.Area)
                    board[i, j] = val;
                g.FillRectangle(brush[GetBoard(p)], new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public void DrawPlayer(Point p)
            {
                int i = p.Y / Constants.Player_Height;
                int j = p.X / Constants.Player_Width;

                g.FillRectangle(brush[3], new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));
            }

            public int GetBoard(Point p)
            {
                int i = p.Y / Constants.Player_Height;
                int j = p.X / Constants.Player_Width;

                return board[i, j];
            }

            public Point GetInnerPoint(Point lt, Point rb)
            {
                Point res = new Point(-1, -1);

                if (lt.Equals(rb) || (lt.X == rb.X || lt.Y == rb.Y))
                    res = lt;
                else
                {
                    res.X = lt.X + Constants.Player_Width;
                    res.Y = lt.Y + Constants.Player_Height;
                }
                return res;
            }

            public void CheckRightBottom(Point p)
            {
                SolidBrush sb = new SolidBrush(Color.Yellow);
                g.FillRectangle(sb, new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));

            }

            public void CheckLeftTop(Point p)
            {
                SolidBrush sb = new SolidBrush(Color.Blue);
                g.FillRectangle(sb, new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));

            }

            public void FillPath(Point p)
            {
                if (p.X < 0 || p.X >= Constants.Board_Width || p.Y < 0 || p.Y >= Constants.Board_Height)
                    return;

                if (GetBoard(p) == Constants.Path)
                    FillArea(p);
            }

            public void FillArea(Point p)
            {
                if (p.X < 0 || p.X >= Constants.Board_Width || p.Y < 0 || p.Y >= Constants.Board_Height)
                    return;

                int key = GetBoard(p);

                switch (key)
                {
                    case Constants.Area: return;
                    case Constants.Path:
                        {
                            DrawRect(p, Constants.Area);
                            FillPath(new Point(p.X - Constants.Player_Width, p.Y)); // left
                            FillPath(new Point(p.X + Constants.Player_Width, p.Y)); // right
                            FillPath(new Point(p.X, p.Y - Constants.Player_Height)); // up
                            FillPath(new Point(p.X, p.Y + Constants.Player_Height)); // down
                            break;
                        }
                    case Constants.Empty:
                        {
                            DrawRect(p, Constants.Area);
                            FillArea(new Point(p.X - Constants.Player_Width, p.Y)); // left
                            FillArea(new Point(p.X + Constants.Player_Width, p.Y)); // right
                            FillArea(new Point(p.X, p.Y - Constants.Player_Height)); // up
                            FillArea(new Point(p.X, p.Y + Constants.Player_Height)); // down
                            FillArea(new Point(p.X - Constants.Player_Width, p.Y - Constants.Player_Height)); // up left
                            FillArea(new Point(p.X + Constants.Player_Width, p.Y - Constants.Player_Height)); // up right
                            FillArea(new Point(p.X - Constants.Player_Width, p.Y + Constants.Player_Height)); // down left
                            FillArea(new Point(p.X + Constants.Player_Width, p.Y + Constants.Player_Height)); // down right
                            break;
                        }
                }
            }
        }

        Player player;
        Board board;
        Button btn_start;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Board_Width+16, Constants.Board_Height+39);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0,0);

            /*
            board = new Board(new Point(this.Left + 12, this.Top + this.Height - this.ClientRectangle.Height + 8));
            this.Controls.Add(board.panel_board);
            //board.DarwBorder();

            player = new Player(new Point(0, 0)); // Start Point
            */

            

            btn_start = new Button();
            btn_start.Size = new Size(100, 100);
            btn_start.Location = new Point(this.Size.Width/2, this.Size.Height/2);
            btn_start.Text = "START";
            btn_start.Click += new EventHandler(BtnStartClick);
            this.Controls.Add(btn_start);

            this.Show();
        }

        void BtnStartClick(Object sender, EventArgs e)
        {
            btn_start.Visible = false;

            board = new Board(new Point(this.Left + 12, this.Top + this.Height - this.ClientRectangle.Height + 8));
            this.Controls.Add(board.panel_board);
            board.DarwBorder();

            player = new Player(new Point(0, 0)); // Start Point
            board.DrawPlayer(player.cur);

            KeyPreview = true;
            this.KeyDown += Key_Down;
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            board.DarwBorder();

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
                board.DrawRect(player.prev, Constants.Path);
                board.DrawPlayer(player.cur);

                if (board.GetBoard(player.prev) == Constants.Area && board.GetBoard(player.cur) == Constants.Empty)
                {
                    player.start = true;
                    player.left_top = new Point(Constants.Board_Width, Constants.Board_Height);
                    player.right_bottom = new Point(0, 0);
                }

                if(player.start)
                {
                    if (board.GetBoard(player.cur) == Constants.Area)
                    {
                        Point innerpoint = board.GetInnerPoint(player.left_top, player.right_bottom);

                        board.CheckLeftTop(player.left_top);
                        board.CheckRightBottom(player.right_bottom);

                        board.FillArea(innerpoint);
                        player.start = false;
                    }
                    player.UpdateLeftTop();
                    player.UpdateRightBottom();
                }
            }
        }
    }  
}
