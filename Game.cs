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
        public Game()
        {
            InitializeComponent();
            this.Size = new Size(600, 500);
            ImageBoard.Size = new Size(570, 450);
            ImageBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            ImageBoard.Image = Image.FromFile("./res/Image/Image1.jpg");
            //ImageBoard.BackgroundImage = Image.FromFile("./res/Image/Image1.jpg");
        }
    }
}
