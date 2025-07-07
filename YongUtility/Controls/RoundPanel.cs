using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YongUtility.Controls
{
    public class RoundPanel : Panel
    {
        public int CornersRadius { get; set; } = 10;

        public int BorderWidth { get; set; } = 5;

        public Color BorderColor { get; set; } = Color.Black;

        public Color NormalColor { get; set; } = Color.White;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // 둥근 모서리 경로 생성
            GraphicsPath path = GetRoundedRectanglePath(new Rectangle(0, 0,
                                                       this.Width, this.Height), CornersRadius);



            Rectangle borderRect = new Rectangle(BorderWidth / 2, BorderWidth / 2, this.Width - BorderWidth - ((BorderWidth + 1) % 2), this.Height - BorderWidth -((BorderWidth + 1) % 2));
            GraphicsPath borderPath = GetRoundedRectanglePath(borderRect, CornersRadius);

            BackColor = Color.Transparent;
            Region = new Region(path);

            // 배경 색상으로 채우기
            using (SolidBrush brush = new SolidBrush(NormalColor))
            {
                g.FillPath(brush, borderPath);
            }
            
            // 테두리 그리기
            if (BorderWidth > 0)
            {
                using (Pen pen = new Pen(BorderColor, BorderWidth))
                {
                    // 테두리가 잘리지 않도록 약간 안쪽으로 그리기
                   
           
                    g.DrawPath(pen, borderPath);
                }
            }
        }
        
        // 둥근 모서리 경로를 생성하는 헬퍼 메서드
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            
            // 각 모서리의 호를 추가
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // 왼쪽 상단
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // 오른쪽 상단
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // 오른쪽 하단
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // 왼쪽 하단
            
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // 크기가 변경될 때 다시 그리기
        }
    }
}
