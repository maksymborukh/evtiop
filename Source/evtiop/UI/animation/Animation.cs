using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UI.animation
{
    public static class Animation
    {

        public static void TranslateX(double from, double to, double duration, UIElement element)
        {
            var fade = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration))
            };

            TranslateTransform translate = new TranslateTransform();
            element.RenderTransform = translate;
            translate.BeginAnimation(TranslateTransform.XProperty, fade);
        }

        public static void TranslateY(double from, double to, double duration, UIElement element)
        {
            var fade = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration))
            };

            TranslateTransform translate = new TranslateTransform();
            element.RenderTransform = translate;
            translate.BeginAnimation(TranslateTransform.YProperty, fade);
        }

        public static void ForegroundColor(Color from, Color to, double duration, TextBlock element)
        {
            var fade = new ColorAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration))
            };
            
            SolidColorBrush brush = new SolidColorBrush();
            element.Foreground = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, fade);
        }

        public static void Rotate(double from, double to, double duration, UIElement element)
        {
            var fade = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration))
            };

            RotateTransform rotate = new RotateTransform();
            element.RenderTransform = rotate;
            rotate.BeginAnimation(RotateTransform.AngleProperty, fade);
        }

    }
}
