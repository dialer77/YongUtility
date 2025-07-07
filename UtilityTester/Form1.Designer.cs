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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.roundedButton1 = new YongUtility.Controls.RoundedButton();
            this.centeredTextBox1 = new YongUtility.Controls.CenteredTextBox();
            this.roundPanel1 = new YongUtility.Controls.RoundPanel();
            this.SuspendLayout();
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(462, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(223, 131);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundedButton1.BorderColor = System.Drawing.Color.Gray;
            this.roundedButton1.BorderRadius = 20;
            this.roundedButton1.BorderWidth = 0;
            this.roundedButton1.ButtonBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.roundedButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.roundedButton1.Location = new System.Drawing.Point(242, 159);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.roundedButton1.Size = new System.Drawing.Size(195, 78);
            this.roundedButton1.TabIndex = 5;
            this.roundedButton1.UseHoverEffect = true;
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
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.BorderColor = System.Drawing.Color.Black;
            this.roundPanel1.BorderWidth = 5;
            this.roundPanel1.CornersRadius = 10;
            this.roundPanel1.Location = new System.Drawing.Point(447, 64);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.NormalColor = System.Drawing.Color.White;
            this.roundPanel1.Size = new System.Drawing.Size(292, 135);
            this.roundPanel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(788, 456);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private YongUtility.Controls.RoundPanel roundPanel1;
    }
}

