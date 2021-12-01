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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.Show();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            String ID = textBox_id.Text;
            int stage = comboBox_stage.SelectedIndex;
            Game game;

            if (!(ID.Equals("") || stage == -1))
                game = new Game(stage);
        }
    }
}
