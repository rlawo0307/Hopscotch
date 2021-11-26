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
            Game gameform = new Game();
            
        }

        private void Btn_Help_Click(object sender, EventArgs e)
        {
            Help helpform = new Help();
            helpform.ShowDialog();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            Setting settingform = new Setting();
            settingform.ShowDialog();
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            ShowFileOpenDialog();
        }

        private string ShowFileOpenDialog()
        {
            //파일 오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 선택";
            ofd.FileName = ".jpg";
            ofd.Filter = "Image File (*.jpg,*.gif, *.bmp) | *.jpg, *.gif, *bmp | 모든 파일 (*.*) | *.*";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string fileName = ofd.SafeFileName;
                string fileFullName = ofd.FileName;
                string filePath = fileFullName.Replace(fileName, "");
            }
            else if (dr == DialogResult.Cancel)
                return "";
            return "";

        }
    }
}
