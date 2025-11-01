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

    public static Image Create(int level = 1)
    {
        return new Image()
        {
            Source = CreateImageSource(SpritePaths[0]),
        };
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