using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

public class BlockAnimator
{
    public void AnimateSpawn(Border blockUI)
    {
        var scaleTransform = new ScaleTransform(0.0, 0.0);
        blockUI.RenderTransformOrigin = new Point(0.5, 0.5);
        blockUI.RenderTransform = scaleTransform;

        var anim = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new BackEase { Amplitude = 0.2, EasingMode = EasingMode.EaseInOut }
        };

        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
    }

    public void AnimateMerge(Border blockUI)
    {
        var scaleTransform = new ScaleTransform(1.0, 1.0);
        blockUI.RenderTransformOrigin = new Point(0.5, 0.5);
        blockUI.RenderTransform = scaleTransform;

        var grow = new DoubleAnimation
        {
            To = 1.2,
            Duration = TimeSpan.FromMilliseconds(150),
            AutoReverse = true,
            EasingFunction = new BackEase { Amplitude = 0.3, EasingMode = EasingMode.EaseOut }
        };

        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, grow);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, grow);
    }
}
