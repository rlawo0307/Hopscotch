
namespace Hopscotch
{
    partial class Hopscotch
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.label_stage = new System.Windows.Forms.Label();
            this.comboBox_stage = new System.Windows.Forms.ComboBox();
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn_Play
            // 
            this.Btn_Play.Location = new System.Drawing.Point(328, 277);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(75, 49);
            this.Btn_Play.TabIndex = 0;
            this.Btn_Play.Text = "PLAY";
            this.Btn_Play.UseVisualStyleBackColor = true;
            this.Btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("맑은 고딕", 50F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.Title.Location = new System.Drawing.Point(58, 73);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(671, 133);
            this.Title.TabIndex = 1;
            this.Title.Text = "HOPSCOTCH";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(560, 277);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(104, 73);
            this.btn_start.TabIndex = 10;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Visible = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label_stage
            // 
            this.label_stage.AutoSize = true;
            this.label_stage.Location = new System.Drawing.Point(90, 327);
            this.label_stage.Name = "label_stage";
            this.label_stage.Size = new System.Drawing.Size(72, 18);
            this.label_stage.TabIndex = 9;
            this.label_stage.Text = "Stage : ";
            this.label_stage.Visible = false;
            // 
            // comboBox_stage
            // 
            this.comboBox_stage.FormattingEnabled = true;
            this.comboBox_stage.Items.AddRange(new object[] {
            "Easy",
            "Normal",
            "Hard"});
            this.comboBox_stage.Location = new System.Drawing.Point(182, 324);
            this.comboBox_stage.Name = "comboBox_stage";
            this.comboBox_stage.Size = new System.Drawing.Size(121, 26);
            this.comboBox_stage.TabIndex = 8;
            this.comboBox_stage.Visible = false;
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(122, 280);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(40, 18);
            this.label_ID.TabIndex = 7;
            this.label_ID.Text = "ID : ";
            this.label_ID.Visible = false;
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(182, 277);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(301, 28);
            this.textBox_id.TabIndex = 6;
            this.textBox_id.Visible = false;
            // 
            // Hopscotch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.label_stage);
            this.Controls.Add(this.comboBox_stage);
            this.Controls.Add(this.label_ID);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Btn_Play);
            this.Name = "Hopscotch";
            this.Text = "Hopscotch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label_stage;
        private System.Windows.Forms.ComboBox comboBox_stage;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.TextBox textBox_id;
    }
}

