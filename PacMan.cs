using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class PacMan : Form
    {
        public PacMan()
        {
            InitializeComponent();
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            Game gameform = new Game();
            gameform.ShowDialog();
        }
    }
}
