using System;
using static MobilePaint.Figure;

namespace MobilePaint
{
    public class XData
    {
        public Android.Graphics.Color Color { get; set; }
        public int Width { get; set; }
        public FType Type { get; set; }

        public XData()
        {
            Color = Android.Graphics.Color.Red;
            Width = 1;
            Type = FType.Curve;
        }
    }
}

