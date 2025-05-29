using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YongUtility.Controls
{
    public partial class CenteredTextBox : UserControl
    {
        private TextBox innerTextBox;
        private bool multiline = false;
        private TextAlignment verticalAlignment = TextAlignment.Center;
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        private string placeholderText = string.Empty;
        private Color placeholderColor = Color.Gray;
        private bool isPlaceholderActive = false;

        public enum TextAlignment
        {
            Top,
            Center,
            Bottom
        }

        public CenteredTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            InitializeInnerTextBox();
            Size = new Size(200, 30);
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.Fixed3D;
        }

        private void InitializeInnerTextBox()
        {
            innerTextBox = new TextBox();
            innerTextBox.BorderStyle = BorderStyle.None;
            innerTextBox.BackColor = BackColor;
            innerTextBox.Font = Font;
            innerTextBox.ForeColor = ForeColor;
            innerTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // 이벤트 연결
            innerTextBox.TextChanged += InnerTextBox_TextChanged;
            innerTextBox.Enter += InnerTextBox_Enter;
            innerTextBox.Leave += InnerTextBox_Leave;
            innerTextBox.KeyDown += InnerTextBox_KeyDown;
            innerTextBox.KeyPress += InnerTextBox_KeyPress;
            innerTextBox.KeyUp += InnerTextBox_KeyUp;

            Controls.Add(innerTextBox);
            UpdateTextBoxPosition();
        }

        #region Properties

        [Category("Appearance")]
        [Description("TextBox에 표시될 텍스트입니다.")]
        public override string Text
        {
            get 
            { 
                if (isPlaceholderActive)
                    return string.Empty;
                return innerTextBox?.Text ?? string.Empty; 
            }
            set
            {
                if (innerTextBox != null)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        ShowPlaceholder();
                    }
                    else
                    {
                        HidePlaceholder();
                        innerTextBox.Text = value;
                    }
                }
            }
        }

        [Category("Behavior")]
        [Description("여러 줄 입력을 허용할지 설정합니다.")]
        public bool Multiline
        {
            get { return multiline; }
            set
            {
                multiline = value;
                if (innerTextBox != null)
                {
                    innerTextBox.Multiline = value;
                    innerTextBox.WordWrap = value;
                    UpdateTextBoxPosition();
                }
            }
        }

        [Category("Appearance")]
        [Description("텍스트의 세로 정렬을 설정합니다.")]
        public TextAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set
            {
                verticalAlignment = value;
                UpdateTextBoxPosition();
            }
        }

        [Category("Appearance")]
        [Description("텍스트의 가로 정렬을 설정합니다.")]
        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set
            {
                horizontalAlignment = value;
                if (innerTextBox != null)
                    innerTextBox.TextAlign = value;
            }
        }

        [Category("Behavior")]
        [Description("텍스트를 읽기 전용으로 설정합니다.")]
        public bool ReadOnly
        {
            get { return innerTextBox?.ReadOnly ?? false; }
            set
            {
                if (innerTextBox != null)
                    innerTextBox.ReadOnly = value;
            }
        }

        [Category("Behavior")]
        [Description("입력 가능한 최대 문자 수를 설정합니다.")]
        public int MaxLength
        {
            get { return innerTextBox?.MaxLength ?? 0; }
            set
            {
                if (innerTextBox != null)
                    innerTextBox.MaxLength = value;
            }
        }

        [Category("Behavior")]
        [Description("비밀번호 입력 시 표시할 문자를 설정합니다.")]
        public char PasswordChar
        {
            get { return innerTextBox?.PasswordChar ?? '\0'; }
            set
            {
                if (innerTextBox != null)
                    innerTextBox.PasswordChar = value;
            }
        }

        [Category("Appearance")]
        [Description("플레이스홀더 텍스트를 설정합니다.")]
        public string PlaceholderText
        {
            get { return placeholderText; }
            set
            {
                placeholderText = value;
                if (string.IsNullOrEmpty(Text))
                {
                    ShowPlaceholder();
                }
            }
        }

        [Category("Appearance")]
        [Description("플레이스홀더 텍스트의 색상을 설정합니다.")]
        public Color PlaceholderColor
        {
            get { return placeholderColor; }
            set
            {
                placeholderColor = value;
                if (isPlaceholderActive)
                {
                    innerTextBox.ForeColor = value;
                }
            }
        }

        #endregion

        #region Placeholder Methods

        private void ShowPlaceholder()
        {
            if (!string.IsNullOrEmpty(placeholderText) && innerTextBox != null)
            {
                isPlaceholderActive = true;
                innerTextBox.Text = placeholderText;
                innerTextBox.ForeColor = placeholderColor;
            }
        }

        private void HidePlaceholder()
        {
            if (isPlaceholderActive && innerTextBox != null)
            {
                isPlaceholderActive = false;
                innerTextBox.Text = string.Empty;
                innerTextBox.ForeColor = ForeColor;
            }
        }

        #endregion

        #region Methods

        private void UpdateTextBoxPosition()
        {
            if (innerTextBox == null) return;

            int padding = 4;
            int textBoxWidth = Width - (padding * 2);
            int textBoxHeight;

            if (multiline)
            {
                textBoxHeight = Height - (padding * 2);
                innerTextBox.Size = new Size(textBoxWidth, textBoxHeight);
                innerTextBox.Location = new Point(padding, padding);
            }
            else
            {
                // 단일 행의 경우 텍스트 높이 계산
                using (Graphics g = CreateGraphics())
                {
                    SizeF textSize = g.MeasureString("Ag", innerTextBox.Font);
                    textBoxHeight = (int)Math.Ceiling(textSize.Height);
                }

                innerTextBox.Size = new Size(textBoxWidth, textBoxHeight);

                // 세로 정렬에 따른 Y 위치 계산
                int y;
                switch (verticalAlignment)
                {
                    case TextAlignment.Top:
                        y = padding;
                        break;
                    case TextAlignment.Center:
                        y = (Height - textBoxHeight) / 2;
                        break;
                    case TextAlignment.Bottom:
                        y = Height - textBoxHeight - padding;
                        break;
                    default:
                        y = (Height - textBoxHeight) / 2;
                        break;
                }

                innerTextBox.Location = new Point(padding, y);
            }
        }

        public void SelectAll()
        {
            if (isPlaceholderActive)
                return;
            innerTextBox?.SelectAll();
        }

        public void Clear()
        {
            if (innerTextBox != null)
            {
                innerTextBox.Clear();
                ShowPlaceholder();
            }
        }

        public new void Focus()
        {
            innerTextBox?.Focus();
        }

        #endregion

        #region Event Handlers

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isPlaceholderActive)
            {
                OnTextChanged(e);
            }
        }

        private void InnerTextBox_Enter(object sender, EventArgs e)
        {
            if (isPlaceholderActive)
            {
                HidePlaceholder();
            }
            OnEnter(e);
        }

        private void InnerTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(innerTextBox.Text))
            {
                ShowPlaceholder();
            }
            OnLeave(e);
        }

        private void InnerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void InnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void InnerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        #endregion

        #region Overrides

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateTextBoxPosition();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (innerTextBox != null)
                innerTextBox.Font = Font;
            UpdateTextBoxPosition();
            base.OnFontChanged(e);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            if (innerTextBox != null && !isPlaceholderActive)
                innerTextBox.ForeColor = ForeColor;
            base.OnForeColorChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (innerTextBox != null)
                innerTextBox.BackColor = BackColor;
            base.OnBackColorChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 테두리 그리기
            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Sunken);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(placeholderText))
            {
                ShowPlaceholder();
            }
        }

        #endregion
    }
} 