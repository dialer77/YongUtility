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
        private BorderStyle borderStyle = BorderStyle.Fixed3D;
        private bool isInitialized = false;

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
            isInitialized = true;
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

        [Category("Appearance")]
        [Description("컨트롤의 테두리 스타일을 설정합니다.")]
        [DefaultValue(BorderStyle.Fixed3D)]
        public new BorderStyle BorderStyle
        {
            get { return borderStyle; }
            set
            {
                if (borderStyle != value)
                {
                    borderStyle = value;
                    if (isInitialized)
                    {
                        UpdateTextBoxPosition();
                        Invalidate();
                        Update(); // 즉시 다시 그리기 강제
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
        [DefaultValue(TextAlignment.Center)]
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
        [DefaultValue(HorizontalAlignment.Left)]
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
        [DefaultValue(typeof(Color), "Gray")]
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
            if (innerTextBox == null || !isInitialized) return;

            // BorderStyle에 따른 패딩 계산
            int padding = GetBorderPadding();
            int textBoxWidth = Math.Max(1, Width - (padding * 2));
            int textBoxHeight;

            if (multiline)
            {
                textBoxHeight = Math.Max(1, Height - (padding * 2));
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
                        y = Math.Max(padding, (Height - textBoxHeight) / 2);
                        break;
                    case TextAlignment.Bottom:
                        y = Math.Max(padding, Height - textBoxHeight - padding);
                        break;
                    default:
                        y = Math.Max(padding, (Height - textBoxHeight) / 2);
                        break;
                }

                innerTextBox.Location = new Point(padding, y);
            }
        }

        private int GetBorderPadding()
        {
            switch (borderStyle)
            {
                case BorderStyle.None:
                    return 2;
                case BorderStyle.FixedSingle:
                    return 3;
                case BorderStyle.Fixed3D:
                    return 4;
                default:
                    return 2;
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
            // 배경 먼저 그리기
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            // BorderStyle에 따른 테두리 그리기
            switch (borderStyle)
            {
                case BorderStyle.None:
                    // 테두리 없음
                    break;
                case BorderStyle.FixedSingle:
                    // 단일 선 테두리
                    using (Pen pen = new Pen(SystemColors.ControlDark))
                    {
                        Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                    break;
                case BorderStyle.Fixed3D:
                    // 3D 테두리
                    ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Sunken);
                    break;
            }

            base.OnPaint(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(placeholderText))
            {
                ShowPlaceholder();
            }
            // 로드 후 다시 그리기
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // 핸들 생성 후 위치 업데이트
            UpdateTextBoxPosition();
            Invalidate();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                UpdateTextBoxPosition();
                Invalidate();
            }
        }

        #endregion
    }
} 