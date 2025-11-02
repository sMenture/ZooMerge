using System.Collections.Generic;


namespace ZooMergeTest.Tests.TestHelpers
{
    /// <summary>
    /// Класс содержит тестовые типы для написания юнит-тестов. 
    /// Он позволяет создавать тестовые блоки, тестовый контейнер
    /// и задавать размеры арены без зависимости от реальных UI-элементов. Иначе, проблемс, ошибка.
    /// </summary>
    public static class DataTest 
    {
        public interface IReadArenaSize
        {
            int SizeX { get; }
            int SizeY { get; }
        }

        public class ArenaSize : IReadArenaSize
        {
            public int SizeX { get; set; }
            public int SizeY { get; set; }
        }


        public class TestBlock
        {
            public int Level { get; set; }
            public int XPosition { get; private set; }
            public int YPosition { get; private set; }

            public TestBlock(int level, int x, int y)
            {
                Level = level;
                XPosition = x;
                YPosition = y;
            }

            public void UpdatePosition(int x, int y)
            {
                XPosition = x;
                YPosition = y;
            }

            public void UpgradeLevel() => Level++;
        }


        public interface IBlockContainer
        {
            IReadOnlyList<TestBlock> Blocks { get; }
            void Add(int xPosition, int yPosition, int level = 1);
            void Remove(TestBlock block);
            void MergeBlocks(TestBlock block1, TestBlock block2);
            TestBlock GetBlockAt(int x, int y);
            void CreateRandomPosition();
        }
    }
}