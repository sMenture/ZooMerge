using System.Windows.Controls;

public class Block
{
    public Block(Image uIElement, int level, int xPosition, int yPosition)
    {
        UIElement = uIElement;
        Level = level;
        XPosition = xPosition;
        YPosition = yPosition;

        UpdatePosition(XPosition, YPosition);
    }

    public Image UIElement { get; private set; }
    public int Level { get; private set; }
    public int XPosition { get; private set; }
    public int YPosition { get; private set; }

    public void UpdatePosition(int x, int y)
    {
        Grid.SetColumn(UIElement, x);
        Grid.SetRow(UIElement, y);

        XPosition = x;
        YPosition = y;
    }

    public void UpgradeLevel()
    {
        if (BlockCreator.ReadOnlyLevelColors.Count <= Level)
            return;

        Level++;

        UIElement.Source = BlockCreator.CreateImageSource(BlockCreator.ReadOnlyLevelColors[Level - 1]);
    }
}