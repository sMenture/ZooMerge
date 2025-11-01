using System.Windows.Controls;

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

        for (int x = 0; x < MapSizeX; x++)
            arena.ColumnDefinitions.Add(new ColumnDefinition());

        for (int y = 0; y < MapSizeY; y++)
            arena.RowDefinitions.Add(new RowDefinition());
    }
}