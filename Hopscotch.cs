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
        public int stage;
        User user;

        public Hopscotch()
        {
            InitializeComponent();

            user = new User();
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            label_ID.Visible = label_stage.Visible = textBox_id.Visible = comboBox_stage.Visible = btn_start.Visible = true;
            Btn_Play.Visible = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            String ID = textBox_id.Text;
            int mode = comboBox_stage.SelectedIndex;
            Game game;

            if (!(ID.Equals("") || mode == -1))
            {
                user.SetID(ID);
                user.SetMode(mode);
                game = new Game(mode, 1);
                game.GameStart();
            }
        }
    }

    public class User
    {
        String ID;
        int mode;

        public User() { }

        public void SetID(String ID) { this.ID = ID; }

        public String GetID() { return ID; }

        public void SetMode(int mode) { this.mode = mode; }

        public int GetMode() { return mode; }
    }
}
