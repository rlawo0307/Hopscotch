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
        public User user;

        public Login()
        {
            InitializeComponent();

            user = new User();

            this.ShowDialog();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            String ID = textBox_id.Text;
            int mode = comboBox_stage.SelectedIndex;

            if (!(ID.Equals("") || mode == -1))
            {
                user.SetID(ID);
                user.SetMode(mode);
                this.Close();
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
