using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace MobilePaint.Droid
{
    [Register("MobilePaint.Droid.PDrawView")]
    public class PDrawView : View
    {
        public PDrawView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
        }

        float x;
        float y;

        public XData data { get; set; }
        List<Figure> figures = new List<Figure>();


        protected override void OnDraw(Canvas canvas)
        {
            canvas.ClipRect(new Rect(0, 0, Width, Height));

            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeJoin = Paint.Join.Round;
            paint.StrokeCap = Paint.Cap.Round;
            paint.AntiAlias = true;

            foreach (Figure f in figures)
            {
                paint.StrokeWidth = f.StrokeWidth;
                paint.Color = f.Color;
                canvas.DrawPath(f.Path, paint);
            }
        }

        private void AddFigure(float curX, float curY)
        {
            Figure figure = new Figure(new PointF(x, y), new PointF(curX, curY), data.Color, data.Width, data.Type);
            figures.Add(figure);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    x = e.GetX();
                    y = e.GetY();
                    break;
                case MotionEventActions.Move:
                    if (data.Type == Figure.FType.Curve)
                    {
                        AddFigure(e.GetX(), e.GetY());
                        x = e.GetX();
                        y = e.GetY();
                    }
                    break;
                case MotionEventActions.Up:
                    AddFigure(e.GetX(), e.GetY());
                    break;
                default:
                    return false;
            }
            Invalidate();
            return true;

        }
    }
}