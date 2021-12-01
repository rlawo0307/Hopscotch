
namespace Hopscotch
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label_ID = new System.Windows.Forms.Label();
            this.comboBox_stage = new System.Windows.Forms.ComboBox();
            this.label_stage = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(206, 190);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(301, 28);
            this.textBox_id.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(146, 193);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(40, 18);
            this.label_ID.TabIndex = 2;
            this.label_ID.Text = "ID : ";
            // 
            // comboBox_stage
            // 
            this.comboBox_stage.FormattingEnabled = true;
            this.comboBox_stage.Items.AddRange(new object[] {
            "Easy",
            "Normal",
            "Hard"});
            this.comboBox_stage.Location = new System.Drawing.Point(206, 237);
            this.comboBox_stage.Name = "comboBox_stage";
            this.comboBox_stage.Size = new System.Drawing.Size(121, 26);
            this.comboBox_stage.TabIndex = 3;
            // 
            // label_stage
            // 
            this.label_stage.AutoSize = true;
            this.label_stage.Location = new System.Drawing.Point(114, 240);
            this.label_stage.Name = "label_stage";
            this.label_stage.Size = new System.Drawing.Size(72, 18);
            this.label_stage.TabIndex = 4;
            this.label_stage.Text = "Stage : ";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(584, 190);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(104, 73);
            this.btn_start.TabIndex = 5;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.label_stage);
            this.Controls.Add(this.comboBox_stage);
            this.Controls.Add(this.label_ID);
            this.Controls.Add(this.textBox_id);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.ComboBox comboBox_stage;
        private System.Windows.Forms.Label label_stage;
        private System.Windows.Forms.Button btn_start;
    }
}