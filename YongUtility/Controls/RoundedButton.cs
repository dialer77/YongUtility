using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YongUtility.Controls
{
    public partial class RoundedButton : UserControl
    {
        private Button innerButton;
        private int borderRadius = 20;
        private Color backgroundColor = Color.FromArgb(52, 152, 219);
        private Color hoverColor = Color.FromArgb(41, 128, 185);
        private Color pressedColor = Color.FromArgb(30, 100, 150);
        private Color borderColor = Color.Transparent;
        private int borderWidth = 0;
        private bool useHoverEffect = true;
        private Color originalBackgroundColor;

        public RoundedButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.UserPaint | 
                     ControlStyles.DoubleBuffer | 
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            
            InitializeInnerButton();
            originalBackgroundColor = backgroundColor;
            Size = new Size(120, 40);
        }

        private void InitializeInnerButton()
        {
            innerButton = new Button();
            innerButton.FlatStyle = FlatStyle.Flat;
            innerButton.FlatAppearance.BorderSize = 0;
            innerButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            innerButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            innerButton.BackColor = backgroundColor;
            innerButton.ForeColor = Color.White;
            innerButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            innerButton.Cursor = Cursors.Hand;
            
            // 이벤트 연결
            innerButton.Click += InnerButton_Click;
            innerButton.MouseEnter += InnerButton_MouseEnter;
            innerButton.MouseLeave += InnerButton_MouseLeave;
            innerButton.MouseDown += InnerButton_MouseDown;
            innerButton.MouseUp += InnerButton_MouseUp;
            
            Controls.Add(innerButton);
        }

        [Category("Appearance")]
        [Description("버튼에 표시될 텍스트입니다.")]
        public override string Text
        {
            get { return innerButton?.Text ?? string.Empty; }
            set 
            { 
                if (innerButton != null)
                    innerButton.Text = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("버튼의 모서리 반지름을 설정합니다.")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("버튼의 기본 배경색을 설정합니다.")]
        public Color ButtonBackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                originalBackgroundColor = value;
                if (innerButton != null)
                    innerButton.BackColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("마우스 호버 시 배경색을 설정합니다.")]
        public Color HoverColor
        {
            get { return hoverColor; }
            set
            {
                hoverColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("버튼 클릭 시 배경색을 설정합니다.")]
        public Color PressedColor
        {
            get { return pressedColor; }
            set
            {
                pressedColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("버튼의 테두리 색상을 설정합니다.")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("버튼의 테두리 두께를 설정합니다.")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("마우스 호버 효과를 사용할지 설정합니다.")]
        public bool UseHoverEffect
        {
            get { return useHoverEffect; }
            set { useHoverEffect = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 내부 버튼 위치와 크기 조정
            int margin = Math.Max(borderWidth, 2);
            innerButton.Location = new Point(margin, margin);
            innerButton.Size = new Size(Width - (margin * 2), Height - (margin * 2));
            innerButton.BackColor = backgroundColor;

            // 둥근 사각형 경로 생성
            GraphicsPath path = GetRoundedRectanglePath(ClientRectangle, borderRadius);

            // 배경 그리기
            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                g.FillPath(brush, path);
            }

            // 테두리 그리기
            if (borderWidth > 0 && borderColor != Color.Transparent)
            {
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    Rectangle borderRect = new Rectangle(
                        borderWidth / 2,
                        borderWidth / 2,
                        Width - borderWidth,
                        Height - borderWidth);
                    GraphicsPath borderPath = GetRoundedRectanglePath(borderRect, borderRadius);
                    g.DrawPath(pen, borderPath);
                }
            }

            // 클리핑 영역 설정 (둥근 모서리 밖의 버튼 부분 숨기기)
            Region = new Region(path);

            path.Dispose();
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            // 모서리가 사각형보다 클 경우 조정
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            // 둥근 모서리 추가
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void InnerButton_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void InnerButton_MouseEnter(object sender, EventArgs e)
        {
            if (useHoverEffect)
            {
                backgroundColor = hoverColor;
                innerButton.BackColor = hoverColor;
                Invalidate();
            }
            OnMouseEnter(e);
        }

        private void InnerButton_MouseLeave(object sender, EventArgs e)
        {
            if (useHoverEffect)
            {
                backgroundColor = originalBackgroundColor;
                innerButton.BackColor = originalBackgroundColor;
                Invalidate();
            }
            OnMouseLeave(e);
        }

        private void InnerButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (useHoverEffect)
            {
                backgroundColor = pressedColor;
                innerButton.BackColor = pressedColor;
                Invalidate();
            }
            OnMouseDown(e);
        }

        private void InnerButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (useHoverEffect)
            {
                backgroundColor = ClientRectangle.Contains(PointToClient(MousePosition)) ? hoverColor : originalBackgroundColor;
                innerButton.BackColor = backgroundColor;
                Invalidate();
            }
            OnMouseUp(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (innerButton != null)
                innerButton.Font = Font;
            base.OnFontChanged(e);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            if (innerButton != null)
                innerButton.ForeColor = ForeColor;
            base.OnForeColorChanged(e);
        }

    }
}
