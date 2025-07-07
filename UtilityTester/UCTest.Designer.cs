namespace UtilityTester
{
    partial class UCTest
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.roundPanel1 = new YongUtility.Controls.RoundPanel();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.BorderColor = System.Drawing.Color.Black;
            this.roundPanel1.BorderWidth = 10;
            this.roundPanel1.CornersRadius = 10;
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.NormalColor = System.Drawing.Color.White;
            this.roundPanel1.Size = new System.Drawing.Size(428, 370);
            this.roundPanel1.TabIndex = 0;
            // 
            // UCTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roundPanel1);
            this.Name = "UCTest";
            this.Size = new System.Drawing.Size(428, 370);
            this.ResumeLayout(false);

        }

        #endregion

        private YongUtility.Controls.RoundPanel roundPanel1;
    }
}
