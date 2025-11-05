using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class BlockCreator
{
    private static readonly string[] SpritePaths = new string[]
    {
        "Sprites/level1.png",
        "Sprites/level2.png",
        "Sprites/level3.png",
        "Sprites/level4.png",
        "Sprites/level5.png",
        "Sprites/level6.png",
        "Sprites/level7.png",
    };

    public static IReadOnlyList<string> ReadOnlyLevelColors => SpritePaths;

    public static Border Create(int level = 1)
    {
        var image = new Image()
        {
            Source = CreateImageSource(SpritePaths[level - 1]),
            Stretch = Stretch.UniformToFill
        };

        var border = new Border()
        {
            Child = image,
            CornerRadius = new CornerRadius(20),
            ClipToBounds = true,
            Margin = new Thickness(10)
        };

        border.Loaded += (s, e) =>
        {
            border.Clip = new RectangleGeometry()
            {
                RadiusX = border.CornerRadius.TopLeft,
                RadiusY = border.CornerRadius.TopLeft,
                Rect = new Rect(0, 0, border.ActualWidth, border.ActualHeight)
            };
        };

        return border;
    }

    public static ImageSource CreateImageSource(string path)
    {
        try
        {
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var bitmap = new System.Windows.Media.Imaging.BitmapImage(uri);
            return bitmap;
        }
        catch
        {
            throw new ArgumentNullException(nameof(path));
        }
    }
}