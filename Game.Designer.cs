
namespace Hopscotch
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageBoard = new System.Windows.Forms.PictureBox();
            this.Btn_Player = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBoard
            // 
            this.ImageBoard.Location = new System.Drawing.Point(12, 12);
            this.ImageBoard.Name = "ImageBoard";
            this.ImageBoard.Size = new System.Drawing.Size(776, 426);
            this.ImageBoard.TabIndex = 0;
            this.ImageBoard.TabStop = false;
            // 
            // Btn_Player
            // 
            this.Btn_Player.Cursor = System.Windows.Forms.Cursors.Default;
            this.Btn_Player.FlatAppearance.BorderSize = 0;
            this.Btn_Player.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Player.Location = new System.Drawing.Point(27, 39);
            this.Btn_Player.Name = "Btn_Player";
            this.Btn_Player.Size = new System.Drawing.Size(75, 44);
            this.Btn_Player.TabIndex = 1;
            this.Btn_Player.Text = "player";
            this.Btn_Player.UseVisualStyleBackColor = true;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_Player);
            this.Controls.Add(this.ImageBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "HopScotch";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBoard;
        private System.Windows.Forms.Button Btn_Player;
    }
}