using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleImage
{
    public partial class UCCircleImage : UserControl
    {
        public UCCircleImage()
        {
            InitializeComponent();
        }

        Image _sourceImage = null;
        public override Image BackgroundImage { get => base.BackgroundImage; set { _sourceImage = value; base.BackgroundImage = CutEllipse(value, new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height), new Size(Width, Height)); } }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (_sourceImage != null)
            {
                base.BackgroundImage = CutEllipse(_sourceImage, new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height), new Size(Width, Height));
            }
            
        }

        //转换为圆形的方法
        private Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }
    }
}
