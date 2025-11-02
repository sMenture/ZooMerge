using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ZooMerge
{
    public class BlockContainer
    {
        private readonly IReadArenaSize ArenaSize;
        private readonly Random _random = new Random();
        private readonly Grid Parent;

        private List<Block> _blocksViewer = new List<Block>();

        public BlockContainer(Grid parent, IReadArenaSize arenaSize)
        {
            Parent = parent;
            ArenaSize = arenaSize;
        }

        public IReadOnlyList<Block> Blocks => _blocksViewer;

        public void Add(int xPosition, int yPosition, int level = 1)
        {
            if (xPosition < 0 || xPosition >= ArenaSize.SizeX)
                throw new ArgumentOutOfRangeException(nameof(xPosition));

            if (yPosition < 0 || yPosition >= ArenaSize.SizeY)
                throw new ArgumentOutOfRangeException(nameof(yPosition));

            if (IsPositionOccupied(xPosition, yPosition))
                throw new InvalidOperationException($"Position ({xPosition}, {yPosition}) is already occupied");

            var uiElement = BlockCreator.Create(level);
            Parent.Children.Add(uiElement);

            var newBlock = new Block(uiElement, level, xPosition, yPosition);
            _blocksViewer.Add(newBlock);
        }

        public void CreateRandomPosition()
        {
            int maxAttempts = 100;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                int x = _random.Next(0, ArenaSize.SizeX);
                int y = _random.Next(0, ArenaSize.SizeY);

                if (IsPositionOccupied(x, y) == false)
                {
                    Add(x, y, 1);
                    return;
                }

                attempts++;
            }
        }

        public void Remove(Block block)
        {
            _blocksViewer.Remove(block);
            Parent.Children.Remove(block.UIElement);
        }

        public void MergeBlocks(Block block1, Block block2)
        {
            block1.UpgradeLevel();
            Remove(block2);
        }

        public Block GetBlockAt(int x, int y)
        {
            return _blocksViewer.FirstOrDefault(block =>
                block.XPosition == x && block.YPosition == y);
        }

        private bool IsPositionOccupied(int xPosition, int yPosition)
        {
            return _blocksViewer.Any(block =>
                block.XPosition == xPosition && block.YPosition == yPosition);
        }
    }
}