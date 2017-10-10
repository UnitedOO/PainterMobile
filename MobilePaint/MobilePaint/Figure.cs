using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
using Android.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePaint
{
    public class Figure
    {
        public enum FType { Curve, Rect, Line, Ellipse }

        public Path Path { get; set; }
        public PointF Start { get; set; }
        public PointF End { get; set; }
        public Color Color { get; set; }
        public int StrokeWidth { get; set; }
        public FType Type { get; set; }

        public Figure(PointF start, PointF end, Color color, int width, FType type)
        {
            Start = start;
            End = end;
            Color = color;
            StrokeWidth = width;
            Type = type;
            Path = CreatePath();
        }

        private Path CreatePath()
        {
            Path path = new Path();
            switch (Type)
            {
                case FType.Curve:
                    path.MoveTo(Start.X, Start.Y);
                    path.LineTo(End.X, End.Y);
                    path.Close();
                    break;
                case FType.Line:
                    path.MoveTo(Start.X, Start.Y);
                    path.LineTo(End.X, End.Y);
                    path.Close();
                    break;
                case FType.Rect:
                    path.AddRect(Start.X, Start.Y, End.X, End.Y, Path.Direction.Cw);
                    path.Close();
                    break;
                case FType.Ellipse:
                    path.AddOval(Start.X, Start.Y, End.X, End.Y, Path.Direction.Cw);
                    //  path.AddCircle(Start.X, Start.Y, 60, Path.Direction.Cw);
                    path.Close();
                    break;
            }
            return path;
        }
    }
}
