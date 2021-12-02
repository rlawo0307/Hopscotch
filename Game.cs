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
        int stage;
        Player player;
        Board board;
        Monster[] monster;
        int monstercnt;

        public Game(int mode, int stage)
        {
            InitializeComponent();

            this.stage = stage;

            KeyPreview = true;
            this.KeyDown += Key_Down;

            timer = new System.Timers.Timer();
            timer.Interval = 1000 / (Constants.Speed + mode * 10);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);

            this.Size = new Size(Constants.Board_Width + 16, Constants.Board_Height + 39);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            board = new Board();
            this.Controls.Add(board.panel_board);

            player = new Player();
        }

        public void GameStart()
        {
            this.Show();
            board.DarwBorder();
            board.DrawRect(player.cur, Constants.Player, player.size);
            monstercnt = Constants.MonsterCnt;
            for (int i = 2; i <= stage; i++)
                monstercnt += 2;
            monster = new Monster[monstercnt];
            Random rand = new Random();
            for (int i = 0; i < monstercnt; i++)
                monster[i] = new Monster(rand);
            timer.Start();
        }

        void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            if (board.empty_cnt < Constants.ClearEmptyCnt)
            {
                if (stage < Constants.TotalStage)
                {
                    MessageBox.Show("Stage Clear");
                    InitStage();
                    stage++;
                    GameStart();
                }
                else if (stage == Constants.TotalStage)
                {
                    MessageBox.Show("Game Clear");
                    timer.Dispose();
                    this.Close();
                }
            }

            for (int i = 0; i < monstercnt; i++)
            {
                board.UpdateBoard(monster[i].cur, Constants.Empty, monster[i].size);
                Point p_tmp = monster[i].GetNextPoint(monster[i].direc);
                int d_tmp = monster[i].direc;
                while (board.GetBoard(p_tmp) == Constants.Area)
                {
                    monster[i].RandDirec(ref d_tmp);
                    p_tmp = monster[i].GetNextPoint(d_tmp);
                }
                int check = board.GetBoard(p_tmp);
                if (check == Constants.Player || check == Constants.Path)
                {
                    board.UpdateBoard(p_tmp, Constants.Over, monster[i].size);
                    Game_Over();
                }
                else
                {
                    monster[i].cur = p_tmp;
                    monster[i].direc = d_tmp;
                    board.UpdateBoard(monster[i].cur, Constants.Monster, monster[i].size);
                }
            }
            timer.Start();
        }

        void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                player.Move(e.KeyCode);
                board.UpdateBoard(player.prev, Constants.Path, player.size);
                board.DrawRect(player.cur, Constants.Player, player.size);
                board.DrawPlayer();D

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

        void Game_Over()
        {
            if (MessageBox.Show("Game Over\n", "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
            {
                InitStage();
                GameStart();
            }
            else
            {
                timer.Dispose();
                this.Close();
            }
        }

        void InitStage()
        {
            board.InitBoard();
            player.InitPlayer();
            for (int i = 0; i < monstercnt; i++)
                monster[i].InitMonster();
        }
    }

    class Board
    {
        public Panel panel_board;
        Graphics g;
        Color[] color = { Color.Black, Color.Red, Color.Green, Color.Yellow, Color.White, Color.Gray }; // Empty, Path, Area, , Monster, Player, Over
        SolidBrush[] brush;
        Pen pen;

        int[,] board;
        int board_row, board_col;
        public int empty_cnt;
        object board_lock;

        public Board()
        {
            board_row = Constants.Board_Height / Constants.Block_Height;
            board_col = Constants.Board_Width / Constants.Block_Width;
            panel_board = new Panel();
            panel_board.Size = new Size(Constants.Board_Width, Constants.Board_Height);
            panel_board.Location = new Point(0, 0);
            panel_board.BackColor = color[Constants.Empty];

            board = new int[board_row, board_col];

            g = panel_board.CreateGraphics();
            brush = new SolidBrush[Constants.Colorcnt];
            for (int i = 0; i < Constants.Colorcnt; i++)
                brush[i] = new SolidBrush(color[i]);
            pen = new Pen(color[Constants.Path]);

            board_lock = new object();

            InitBoard();
        }

        public void InitBoard()
        {
            for (int i = 0; i < board_row; i++)
                for (int j = 0; j < board_col; j++)
                    board[i, j] = Constants.Empty;
            DrawRect(new Point(0, 0), Constants.Empty, new Size(Constants.Board_Width, Constants.Board_Height));
            empty_cnt = (board_row-2) * (board_col-2);
        }

        public void DarwBorder()
        {
            Point tmp = new Point(0, 0);
            Size blocksize = new Size(Constants.Block_Width, Constants.Block_Height);

            for (int i = 0; i < board_col; i++)
            {
                tmp.X = i * Constants.Block_Width;

                tmp.Y = 0;
                UpdateBoard(tmp, Constants.Area, blocksize);
                tmp.Y = Constants.Board_Height - Constants.Block_Height;
                UpdateBoard(tmp, Constants.Area, blocksize);
            }
            for (int i = 0; i < board_row; i++)
            {
                tmp.Y = i * Constants.Block_Height;

                tmp.X = 0;
                UpdateBoard(tmp, Constants.Area, blocksize);
                tmp.X = Constants.Board_Width - Constants.Block_Width;
                UpdateBoard(tmp, Constants.Area, blocksize);
            }

        }

        public void UpdateBoard(Point p, int val, Size size)
        {
            int i = p.Y / Constants.Block_Height;
            int j = p.X / Constants.Block_Width;

            if (val == Constants.Over)
                lock(board_lock)
                    board[i, j] = val;
            else
                if (board[i,j] != Constants.Area)
                    lock (board_lock)
                        board[i, j] = val;
            DrawRect(p, board[i,j], size);

            if (val == Constants.Area)
                empty_cnt--;
        }

        public int GetBoard(Point p)
        {
            int i = p.Y / Constants.Block_Height;
            int j = p.X / Constants.Block_Width;
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
                        while (GetBoard(new Point(left.X - Constants.Block_Width, left.Y)) == Constants.Empty)
                        {
                            left.X -= Constants.Block_Width;
                            leftcnt++;
                        }
                        while (GetBoard(new Point(right.X + Constants.Block_Width, right.Y)) == Constants.Empty)
                        {
                            right.X += Constants.Block_Width;
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
                        while (GetBoard(new Point(up.X, up.Y - Constants.Block_Height)) == Constants.Empty)
                        {
                            up.Y -= Constants.Block_Height;
                            upcnt++;
                        }
                        while (GetBoard(new Point(down.X, down.Y + Constants.Block_Height)) == Constants.Empty)
                        {
                            down.Y += Constants.Block_Height;
                            downcnt++;
                        }
                        if (upcnt < downcnt)
                            lt.Y = up.Y;
                        else
                            rb.Y = down.Y;
                    }
                    else
                        lt.Y = e.Y;
                res.X = (lt.X + rb.X) / 2 / Constants.Block_Width * Constants.Block_Width;
                res.Y = (lt.Y + rb.Y) / 2 / Constants.Block_Height * Constants.Block_Height;
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

            Size blocksize = new Size(Constants.Block_Width, Constants.Block_Height);
            int key = GetBoard(p);

            switch (key)
            {
                case Constants.Area: return;
                case Constants.Path:
                    {
                        UpdateBoard(p, Constants.Area, blocksize);
                        FillPath(new Point(p.X - Constants.Block_Width, p.Y)); // left
                        FillPath(new Point(p.X + Constants.Block_Width, p.Y)); // right
                        FillPath(new Point(p.X, p.Y - Constants.Block_Height)); // up
                        FillPath(new Point(p.X, p.Y + Constants.Block_Height)); // down
                        break;
                    }
                case Constants.Empty:
                    {
                        UpdateBoard(p, Constants.Area, blocksize);
                        FillArea(new Point(p.X - Constants.Block_Width, p.Y)); // left
                        FillArea(new Point(p.X + Constants.Block_Width, p.Y)); // right
                        FillArea(new Point(p.X, p.Y - Constants.Block_Height)); // up
                        FillArea(new Point(p.X, p.Y + Constants.Block_Height)); // down
                        FillArea(new Point(p.X - Constants.Block_Width, p.Y - Constants.Block_Height)); // up left
                        FillArea(new Point(p.X + Constants.Block_Width, p.Y - Constants.Block_Height)); // up right
                        FillArea(new Point(p.X - Constants.Block_Width, p.Y + Constants.Block_Height)); // down left
                        FillArea(new Point(p.X + Constants.Block_Width, p.Y + Constants.Block_Height)); // down right
                        break;
                    }
            }
        }

        public void DrawRect(Point p, int val, Size size)
        {
            lock (board_lock)
                g.FillRectangle(brush[val], new Rectangle(p.X, p.Y, size.Width, size.Height));
        }

        public void DrawPlayer(Point p, Keys d, Size size)
        {
            lock (board_lock)
            {
                g.FillRectangle(brush[Constants.Player], new Rectangle(p.X, p.Y, size.Width, size.Height));
                switch(d)
                {
                    case Constants.Left:  break;
                    case Constants.Right: break;
                    case Constants.Down: break;
                        
                    case Constants.Up: break;
                }
                g.DrawLine(pen, );
            }
        }
    }

    class Player
    {
        public Point cur;
        public Point prev;
        public Keys direc;

        public Point start;
        public Point end;
        public Point left_top;
        public Point right_bottom;
        public Size size;

        public bool draw;
        

        public Player()
        {
            size = new Size(Constants.Block_Width, Constants.Block_Height);
            InitPlayer();
        }

        public void InitPlayer()
        {
            cur = prev = new Point(Constants.StartX, Constants.StartY);
            direc = 0;
            draw = false;
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

        public void Move(Keys key)
        {
            if (direc != key)
                direc = key;
            else
            {
                prev = cur;
                switch (key)
                {
                    case Constants.Left: if (cur.X > 0) cur.X -= Constants.Block_Width; break;
                    case Constants.Right: if (cur.X + Constants.Block_Width < Constants.Board_Width) cur.X += Constants.Block_Width; break;
                    case Constants.Down: if (cur.Y + Constants.Block_Height < Constants.Board_Height) cur.Y += Constants.Block_Height; break;
                    case Constants.Up: if (cur.Y > 0) cur.Y -= Constants.Block_Height; break;
                }
            }
        }
    }

    class Monster
    {
        public Point cur;
        public Size size;
        public int direc;
        Random rand;

        public Monster(Random rand)
        {
            this.rand = rand;
            size = new Size(Constants.Monster_Width, Constants.Monster_Height);
            InitMonster();
            /*
            if (n == totalcnt - 1)
                size = new Size(Constants.Boss_Width, Constants.Boss_Height);
            else
                size = new Size(Constants.Monster_Width, Constants.Monster_Height);
            */
            //game_over = false;
        }

        public void InitMonster()
        {
            RandPoint();
            RandDirec(ref direc);
        }

        public Point GetNextPoint(int d)
        {
            Point res = cur;
            switch (d)
            {
                case 1: res.X -= size.Width; res.Y -= size.Height; break; //left-up
                case 2: res.Y -= size.Height; break; //up
                case 3: res.X += size.Width; res.Y -= size.Height; break; //right-up
                case 4: res.X -= size.Width; break; //left
                case 5: res.X += size.Width; break; //right
                case 6: res.X -= size.Width; res.Y += size.Height; break; //left-down
                case 7: res.Y -= size.Height; break; //down
                case 8: res.X += size.Width; res.Y += size.Height; break; // right-down
            }
            return res;
        }

        void RandPoint()
        {
            int x = rand.Next(Constants.Block_Width, Constants.Board_Width - Constants.Block_Width) / Constants.Block_Width * Constants.Block_Width;
            int y = rand.Next(Constants.Block_Height, Constants.Board_Height - Constants.Block_Height) / Constants.Block_Height * Constants.Block_Height;
            cur = new Point(x, y);
        }

        public void RandDirec(ref int d)
        {
            d = rand.Next(1, 9 * 9 * 9) % 8 + 1;
        }
    }

    static class Constants
    {
        //public const String MainImage = "./res/Image/Main.bmp";

        public const int Board_Width = 600, Board_Height = 500;
        public const int Block_Width = 10, Block_Height = 10;
        public const int Monster_Width = 10, Monster_Height = 10;
       // public const int Boss_Width = 100, Boss_Height = 100;

        public const int Empty = 0;
        public const int Path = 1;
        public const int Area = 2;
        public const int Monster = 3;
        public const int Player = 4;
        public const int Over = 5;

        public const Keys Left = Keys.A;
        public const Keys Right = Keys.D;
        public const Keys Down = Keys.S;
        public const Keys Up = Keys.W;

        public const int Colorcnt = 6;
        public const int MonsterCnt = 3;
        public const int TotalStage = 2;

        public const double Speed = 10;
        public const int ClearEmptyCnt = 1000;

        public const int StartX = 0;
        public const int StartY = 0;
    }
}