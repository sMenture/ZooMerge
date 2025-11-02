using System.Linq;

namespace ZooMerge
{
    public class BlockMover : IReadOnlyBlockMover
    {
        private readonly IReadArenaSize ArenaSize;
        private readonly BlockContainer BlockContainer;

        public BlockMover(BlockContainer blockContainer, IReadArenaSize arenaSize)
        {
            BlockContainer = blockContainer;
            ArenaSize = arenaSize;
        }

        public void MoveHorizontal(int step)
        {
            if (step == 0) return;

            bool moved = false;
            bool merged = false;

            var sortedBlocks = step > 0
                ? BlockContainer.Blocks.OrderByDescending(b => b.XPosition)
                : BlockContainer.Blocks.OrderBy(b => b.XPosition);

            foreach (var block in sortedBlocks.ToList())
            {
                int newX = block.XPosition;
                bool canMove = true;

                while (canMove)
                {
                    int nextX = newX + step;

                    if (nextX < 0 || nextX >= ArenaSize.SizeX)
                    {
                        canMove = false;
                        break;
                    }

                    var targetBlock = BlockContainer.GetBlockAt(nextX, block.YPosition);

                    if (targetBlock == null)
                    {
                        newX = nextX;
                        moved = true;
                    }
                    else if (targetBlock.Level == block.Level && targetBlock != block)
                    {
                        BlockContainer.MergeBlocks(targetBlock, block);
                        newX = nextX;
                        merged = true;
                        canMove = false;
                    }
                    else
                        canMove = false;
                }

                if (newX != block.XPosition && BlockContainer.Blocks.Contains(block))
                {
                    block.UpdatePosition(newX, block.YPosition);
                }
            }

            if (moved || merged)
                BlockContainer.CreateRandomPosition();
        }

        public void MoveVertical(int step)
        {
            if (step == 0) return;

            bool moved = false;
            bool merged = false;

            var sortedBlocks = step > 0
                ? BlockContainer.Blocks.OrderByDescending(b => b.YPosition)
                : BlockContainer.Blocks.OrderBy(b => b.YPosition);

            foreach (var block in sortedBlocks.ToList())
            {
                int newY = block.YPosition;
                bool canMove = true;

                while (canMove)
                {
                    int nextY = newY + step;

                    if (nextY < 0 || nextY >= ArenaSize.SizeY)
                    {
                        canMove = false;
                        break;
                    }

                    var targetBlock = BlockContainer.GetBlockAt(block.XPosition, nextY);

                    if (targetBlock == null)
                    {
                        newY = nextY;
                        moved = true;
                    }
                    else if (targetBlock.Level == block.Level && targetBlock != block)
                    {
                        BlockContainer.MergeBlocks(targetBlock, block);
                        newY = nextY;
                        merged = true;
                        canMove = false;
                    }
                    else
                        canMove = false;
                }

                if (newY != block.YPosition && BlockContainer.Blocks.Contains(block))
                {
                    block.UpdatePosition(block.XPosition, newY);
                }
            }

            if (moved || merged)
                BlockContainer.CreateRandomPosition();
        }
    }
}