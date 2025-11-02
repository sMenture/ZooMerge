using System.Linq;

public class BlockMover : IReadOnlyBlockMover
{
    private readonly IReadArenaSize ArenaSize;
    private readonly BlockContainer BlockContainer;

    public BlockMover(BlockContainer blockContainer, IReadArenaSize arenaSize)
    {
        BlockContainer = blockContainer;
        ArenaSize = arenaSize;
    }

    public void MoveHorizontal(int step) => Move(step, true);

    public void MoveVertical(int step) => Move(step, false);

    private void Move(int step, bool isHorizontal)
    {
        if (step == 0) return;

        bool moved = false;
        bool merged = false;

        var sortedBlocks = GetSortedBlocks(step, isHorizontal);

        foreach (var block in sortedBlocks.ToList())
        {
            var (newPosition, wasMoved, wasMerged) = CalculateNewPosition(block, step, isHorizontal);

            moved |= wasMoved;
            merged |= wasMerged;

            if (newPosition != GetCurrentPosition(block, isHorizontal) && BlockContainer.Blocks.Contains(block))
            {
                UpdatePosition(block, newPosition, isHorizontal);
            }
        }

        if (moved || merged)
            BlockContainer.CreateRandomPosition();
    }

    private IOrderedEnumerable<Block> GetSortedBlocks(int step, bool isHorizontal)
    {
        if (isHorizontal)
        {
            return step > 0
                ? BlockContainer.Blocks.OrderByDescending(b => b.XPosition)
                : BlockContainer.Blocks.OrderBy(b => b.XPosition);
        }

        return step > 0
            ? BlockContainer.Blocks.OrderByDescending(b => b.YPosition)
            : BlockContainer.Blocks.OrderBy(b => b.YPosition);
    }

    private (int newPosition, bool moved, bool merged) CalculateNewPosition(Block block, int step, bool isHorizontal)
    {
        int currentPosition = GetCurrentPosition(block, isHorizontal);
        int newPosition = currentPosition;
        bool moved = false;
        bool merged = false;

        bool canMove = true;
        while (canMove)
        {
            int nextPosition = newPosition + step;

            if (!IsPositionValid(nextPosition, isHorizontal))
            {
                canMove = false;
                break;
            }

            var targetBlock = GetTargetBlock(block, nextPosition, isHorizontal);

            if (targetBlock == null)
            {
                newPosition = nextPosition;
                moved = true;
            }
            else if (targetBlock.Level == block.Level && targetBlock != block)
            {
                BlockContainer.MergeBlocks(targetBlock, block);
                newPosition = nextPosition;
                merged = true;
                canMove = false;
            }
            else
                canMove = false;
        }

        return (newPosition, moved, merged);
    }

    private bool IsPositionValid(int position, bool isHorizontal)
    {
        int maxSize = isHorizontal ? ArenaSize.SizeX : ArenaSize.SizeY;
        return position >= 0 && position < maxSize;
    }

    private Block GetTargetBlock(Block block, int nextPosition, bool isHorizontal)
    {
        return isHorizontal
            ? BlockContainer.GetBlockAt(nextPosition, block.YPosition)
            : BlockContainer.GetBlockAt(block.XPosition, nextPosition);
    }

    private int GetCurrentPosition(Block block, bool isHorizontal)
    {
        return isHorizontal ? block.XPosition : block.YPosition;
    }

    private void UpdatePosition(Block block, int newPosition, bool isHorizontal)
    {
        if (isHorizontal)
            block.UpdatePosition(newPosition, block.YPosition);
        else
            block.UpdatePosition(block.XPosition, newPosition);
    }
}