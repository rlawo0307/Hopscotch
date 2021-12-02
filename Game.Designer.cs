
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
            this.label_chance = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.label_stage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_chance
            // 
            this.label_chance.AutoSize = true;
            this.label_chance.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chance.Location = new System.Drawing.Point(726, 37);
            this.label_chance.Name = "label_chance";
            this.label_chance.Size = new System.Drawing.Size(51, 28);
            this.label_chance.TabIndex = 1;
            this.label_chance.Text = "♥ : ";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ID.Location = new System.Drawing.Point(726, 9);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(52, 28);
            this.label_ID.TabIndex = 2;
            this.label_ID.Text = "ID : ";
            // 
            // label_stage
            // 
            this.label_stage.AutoSize = true;
            this.label_stage.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label_stage.Location = new System.Drawing.Point(297, 9);
            this.label_stage.Name = "label_stage";
            this.label_stage.Size = new System.Drawing.Size(175, 41);
            this.label_stage.TabIndex = 3;
            this.label_stage.Text = "< Stage > ";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_stage);
            this.Controls.Add(this.label_ID);
            this.Controls.Add(this.label_chance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "HopScotch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_chance;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label_stage;
    }
}