using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopscotch
{
    public partial class Game : Form
    {
        public System.Timers.Timer timer;
        Player player;
        Board board;
        Button btn_start;
        Monster monster;

        public Game()
        {
            InitializeComponent();

            this.Size = new Size(Constants.Board_Width + 16, Constants.Board_Height + 39);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            btn_start = new Button();
            btn_start.Size = new Size(100, 100);
            btn_start.Location = new Point(this.Size.Width / 2, this.Size.Height / 2);
            btn_start.Text = "START";
            btn_start.Click += new EventHandler(BtnStartClick);
            this.Controls.Add(btn_start);

            this.Show();
        }

        void BtnStartClick(Object sender, EventArgs e)
        {
            btn_start.Visible = false;

            board = new Board();
            this.Controls.Add(board.panel_board);
            board.DarwBorder();

            player = new Player(new Point(0, 0)); // Start Point
            board.DrawRect(player.cur, Constants.Player);

            monster = new Monster(board);

            KeyPreview = true;
            this.KeyDown += Key_Down;

            timer = new System.Timers.Timer();
            timer.Interval = 1000 / Constants.Speed; //1sec
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);
            timer.Start();
        }

        void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            if (board.empty_cnt < Constants.ClearEmptyCnt)
            {
                MessageBox.Show("Game Clear");
                this.Close();
            }
            else if (monster.game_over)
            {
                MessageBox.Show("Game Over");
                this.Close();
            }
            else
            {
                monster.MoveMonster();
                timer.Start();
            }
        }

        void Key_Down(object sender, KeyEventArgs e)
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
                board.UpdateBoard(player.prev, Constants.Path);
                board.DrawRect(player.cur, Constants.Player);

                if (board.GetBoard(player.prev) == Constants.Area && board.GetBoard(player.cur) == Constants.Empty)
                {
                    player.start = player.cur;
                    player.draw = true;
                    player.left_top = new Point(Constants.Board_Width, Constants.Board_Height);
                    player.right_bottom = new Point(0, 0);
                }
                if (player.draw)
                {
                    if (board.GetBoard(player.cur) == Constants.Area)
                    {
                        player.end = player.prev;
                        board.FillArea(board.GetInnerPoint(player.start, player.end, ref player.left_top, ref player.right_bottom));
                        player.draw = false;
                    }
                    player.UpdateLeftTop();
                    player.UpdateRightBottom();
                }
            }

        }
    }

    class Board
    {
        public Panel panel_board;
        Graphics g;
        Color[] color = { Color.Black, Color.Gray, Color.Green, Color.Yellow, Color.White, Color.Red }; // Empty, Path, Area, , Monster, Player, Over
        SolidBrush[] brush;

        int[,] board;
        int board_row, board_col;
        public int empty_cnt;
        object board_lock;

        public Board()
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
            empty_cnt = board_row * board_col;

            g = panel_board.CreateGraphics();
            brush = new SolidBrush[Constants.Colorcnt];
            for (int i = 0; i < Constants.Colorcnt; i++)
                brush[i] = new SolidBrush(color[i]);

            board_lock = new object();
        }

        public void UpdateBoard(Point p, int val)
        {
            int i = p.Y / Constants.Player_Height;
            int j = p.X / Constants.Player_Width;

            if (val == Constants.Over)
                lock(board_lock)
                    board[i, j] = val;
            else
                if (board[i,j] != Constants.Area)
                    lock (board_lock)
                        board[i, j] = val;
            DrawRect(p, board[i,j]);

            if (val == Constants.Area)
                empty_cnt--;
        }

        public int GetBoard(Point p)
        {
            int i = p.Y / Constants.Player_Height;
            int j = p.X / Constants.Player_Width;
            int res;

            lock (board_lock)
                res = board[i, j];

            return res;
        }

        public Point GetInnerPoint(Point s, Point e, ref Point lt, ref Point rb)
        {
            Point res = new Point(-1, -1);

            if (s.Equals(e))
                res = s;
            else
            {
                if (lt.X == rb.X)
                    if (lt.X == e.X)
                    {
                        Point left = lt, right = lt;
                        int leftcnt = 0, rightcnt = 0;
                        while (GetBoard(new Point(left.X - Constants.Player_Width, left.Y)) == Constants.Empty)
                        {
                            left.X -= Constants.Player_Width;
                            leftcnt++;
                        }
                        while (GetBoard(new Point(right.X + Constants.Player_Width, right.Y)) == Constants.Empty)
                        {
                            right.X += Constants.Player_Width;
                            rightcnt++;
                        }
                        if (leftcnt < rightcnt)
                            lt.X = left.X;
                        else
                            rb.X = right.X;
                    }
                    else
                        lt.X = e.X;
                else if (lt.Y == rb.Y)
                    if (lt.Y == e.Y)
                    {
                        Point up = lt, down = lt;
                        int upcnt = 0, downcnt = 0;
                        while (GetBoard(new Point(up.X, up.Y - Constants.Player_Height)) == Constants.Empty)
                        {
                            up.Y -= Constants.Player_Height;
                            upcnt++;
                        }
                        while (GetBoard(new Point(down.X, down.Y + Constants.Player_Height)) == Constants.Empty)
                        {
                            down.Y += Constants.Player_Height;
                            downcnt++;
                        }
                        if (upcnt < downcnt)
                            lt.Y = up.Y;
                        else
                            rb.Y = down.Y;
                    }
                    else
                        lt.Y = e.Y;
                res.X = (lt.X + rb.X) / 2 / Constants.Player_Width * Constants.Player_Width;
                res.Y = (lt.Y + rb.Y) / 2 / Constants.Player_Height * Constants.Player_Height;
            }
            return res;
        }

        void FillPath(Point p)
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
                        UpdateBoard(p, Constants.Area);
                        FillPath(new Point(p.X - Constants.Player_Width, p.Y)); // left
                        FillPath(new Point(p.X + Constants.Player_Width, p.Y)); // right
                        FillPath(new Point(p.X, p.Y - Constants.Player_Height)); // up
                        FillPath(new Point(p.X, p.Y + Constants.Player_Height)); // down
                        break;
                    }
                case Constants.Empty:
                    {
                        UpdateBoard(p, Constants.Area);
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

        public void DarwBorder()
        {
            Point tmp = new Point(0, 0);
            for (int i = 0; i < board_col; i++)
            {
                tmp.X = i * Constants.Player_Width;

                tmp.Y = 0;
                UpdateBoard(tmp, Constants.Area);
                tmp.Y = Constants.Board_Height - Constants.Player_Height;
                UpdateBoard(tmp, Constants.Area);
            }
            for (int i = 0; i < board_row; i++)
            {
                tmp.Y = i * Constants.Player_Height;

                tmp.X = 0;
                UpdateBoard(tmp, Constants.Area);
                tmp.X = Constants.Board_Width - Constants.Player_Width;
                UpdateBoard(tmp, Constants.Area);
            }

        }

        public void DrawRect(Point p, int val)
        {
            lock(board_lock)
                g.FillRectangle(brush[val], new Rectangle(p.X, p.Y, Constants.Player_Width, Constants.Player_Height));
        }
    }

    class Player
    {
        public Point cur;
        public Point prev;
        public Point start;
        public Point end;
        public Point left_top;
        public Point right_bottom;
        public bool draw;

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

    class Monster
    {
        Random rand_p;
        Random rand_d;
        Board board;
        int mcnt;
        Point[] cur;
        int[] direc;
        public bool game_over;

        public Monster(Board board)
        {
            rand_p = new Random();
            rand_d = new Random();

            this.board = board;
            mcnt = Constants.MonsterCnt;
            cur = new Point[mcnt];
            direc = new int[mcnt];
            for (int i = 0; i < mcnt; i++)
            {
                RandPoint(rand_p, ref cur[i]);
                RandDirec(rand_d, ref direc[i]);
            }
            game_over = false;
        }

        public void MoveMonster()
        {
            for (int i = 0; i < mcnt; i++)
            {
                board.UpdateBoard(cur[i], Constants.Empty);
                Point tmp = GetNextPoint(cur[i], direc[i]);
                while (board.GetBoard(tmp) == Constants.Area)
                {
                    RandDirec(rand_d, ref direc[i]);
                    tmp = GetNextPoint(cur[i], direc[i]);
                }
                cur[i] = tmp;
                if (ReachPlayer(tmp))
                {
                    board.UpdateBoard(cur[i], Constants.Over);
                    game_over = true;
                }
                else
                    board.UpdateBoard(cur[i], Constants.Monster);
            }
        }

        bool ReachPlayer(Point p)
        {
            int data = board.GetBoard(p);

            if (data == Constants.Path || data == Constants.Player)
                return true;
            return false;
        }

        Point GetNextPoint(Point p, int d)
        {
            Point res = p;
            switch (d)
            {
                case 1: res.X -= Constants.Player_Width; res.Y -= Constants.Player_Height; break; //left-up
                case 2: res.Y -= Constants.Player_Height; break; //up
                case 3: res.X += Constants.Player_Width; res.Y -= Constants.Player_Height; break; //right-up
                case 4: res.X -= Constants.Player_Width; break; //left
                case 5: res.X += Constants.Player_Width; break; //right
                case 6: res.X -= Constants.Player_Width; res.Y += Constants.Player_Height; break; //left-down
                case 7: res.Y -= Constants.Player_Height; break; //down
                case 8: res.X += Constants.Player_Width; res.Y += Constants.Player_Height; break; // right-down
            }
            return res;
        }

        void RandPoint(Random rand, ref Point p)
        {
            int x = rand.Next(Constants.Player_Width, Constants.Board_Width - Constants.Player_Width) / Constants.Player_Width * Constants.Player_Width;
            int y = rand.Next(Constants.Player_Height, Constants.Board_Height - Constants.Player_Height) / Constants.Player_Height * Constants.Player_Height;
            p = new Point(x, y);
        }

        void RandDirec(Random rand, ref int d)
        {
            d = rand.Next(1, 9 * 9 * 9) % 8 + 1;
        }
    }

    static class Constants
    {
        public const int Board_Width = 600, Board_Height = 500;
        public const int Player_Width = 10, Player_Height = 10;

        public const int Empty = 0;
        public const int Path = 1;
        public const int Area = 2;
        public const int Monster = 3;
        public const int Player = 4;
        public const int Over = 5;

        public const int Colorcnt = 6;
        public const int MonsterCnt = 2;

        public const double Speed = 20;
        public const int ClearEmptyCnt = 1000;
    }
}