using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class BlockArena : IReadArenaSize
{
    private readonly int MapSizeX;
    private readonly int MapSizeY;

    public BlockArena(int sizeX, int sizeY)
    {
        MapSizeX = sizeX;
        MapSizeY = sizeY;
    }

    public int SizeX => MapSizeX;
    public int SizeY => MapSizeY;

    public void CreateMap(Grid arena)
    {
        arena.ColumnDefinitions.Clear();
        arena.RowDefinitions.Clear();
        arena.Children.Clear();

        for (int x = 0; x < MapSizeX; x++)
            arena.ColumnDefinitions.Add(new ColumnDefinition());

        for (int y = 0; y < MapSizeY; y++)
            arena.RowDefinitions.Add(new RowDefinition());

        for (int x = 0; x < MapSizeX; x++)
            for (int y = 0; y < MapSizeY; y++)
            {
                var cellBorder = new Border()
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255)),
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(15),
                    Background = Brushes.Transparent
                };
                Grid.SetColumn(cellBorder, x);
                Grid.SetRow(cellBorder, y);
                arena.Children.Add(cellBorder);
            }
    }
}