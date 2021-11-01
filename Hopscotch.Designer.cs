
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
            this.Btn_Help = new System.Windows.Forms.Button();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Play
            // 
            this.Btn_Play.Location = new System.Drawing.Point(134, 262);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(75, 49);
            this.Btn_Play.TabIndex = 0;
            this.Btn_Play.Text = "PLAY";
            this.Btn_Play.UseVisualStyleBackColor = true;
            this.Btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // Btn_Help
            // 
            this.Btn_Help.Location = new System.Drawing.Point(275, 262);
            this.Btn_Help.Name = "Btn_Help";
            this.Btn_Help.Size = new System.Drawing.Size(75, 49);
            this.Btn_Help.TabIndex = 1;
            this.Btn_Help.Text = "HELP";
            this.Btn_Help.UseVisualStyleBackColor = true;
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.Location = new System.Drawing.Point(418, 262);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(97, 49);
            this.Btn_Setting.TabIndex = 2;
            this.Btn_Setting.Text = "SETTING";
            this.Btn_Setting.UseVisualStyleBackColor = true;
            // 
            // PacMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_Setting);
            this.Controls.Add(this.Btn_Help);
            this.Controls.Add(this.Btn_Play);
            this.Name = "PacMan";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Help;
        private System.Windows.Forms.Button Btn_Setting;
    }
}

