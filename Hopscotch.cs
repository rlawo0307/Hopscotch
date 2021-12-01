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
        public Hopscotch()
        {
            InitializeComponent();
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MessageBox.Show("dd");

            /*
            Btn_Play.Visible = false;

            TextBox Tb_Id = new TextBox();
            Tb_Id.Size = new Size(100, 100);
            Tb_Id.Location = new Point(10, 10);
            Tb_Id.Text = "ID : ";
            this.Controls.Add(Tb_Id);

            ComboBox stage = new ComboBox();
            stage.Text = "(Select Stage)";
            for (int i = 0; i < 5; i++)
            {
                String str = "Stage ";
                stage.Items.Add(str + (i+1));
            }
            stage.Size = new Size(150, 100);
            stage.Location = new Point(Btn_Play.Location.X, Btn_Play.Location.Y + Btn_Play.Height + 10);
            this.Controls.Add(stage);
            */
            
        }      
    }
}
