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
    public partial class Hopscotch : Form
    {
        public Game game;
        public int stage;
        int prev_stage;

        public Hopscotch()
        {
            InitializeComponent();
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Login login = new Login();

            game = new Game(login.user.GetMode(), 1);
            game.GameStart();
        }      
    }
}
