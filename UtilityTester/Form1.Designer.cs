namespace UtilityTester
{
    partial class Form1
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
            this.centeredTextBox1 = new YongUtility.Controls.CenteredTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.roundedButton1 = new YongUtility.Controls.RoundedButton();
            this.SuspendLayout();
            // 
            // centeredTextBox1
            // 
            this.centeredTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.centeredTextBox1.Location = new System.Drawing.Point(19, 176);
            this.centeredTextBox1.MaxLength = 32767;
            this.centeredTextBox1.Multiline = false;
            this.centeredTextBox1.Name = "centeredTextBox1";
            this.centeredTextBox1.PasswordChar = '\0';
            this.centeredTextBox1.PlaceholderText = "";
            this.centeredTextBox1.ReadOnly = false;
            this.centeredTextBox1.Size = new System.Drawing.Size(141, 61);
            this.centeredTextBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(242, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 93);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BorderColor = System.Drawing.Color.Gray;
            this.roundedButton1.BorderRadius = 20;
            this.roundedButton1.Location = new System.Drawing.Point(276, 131);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(195, 78);
            this.roundedButton1.TabIndex = 5;
            this.roundedButton1.Text = "roundedButton1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(617, 456);
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.centeredTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private YongUtility.Controls.CenteredTextBox centeredTextBox1;
        private System.Windows.Forms.Button button1;
        private YongUtility.Controls.RoundedButton roundedButton1;
    }
}

