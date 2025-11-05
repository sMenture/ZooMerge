using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Controls;
using ZooMerge;

namespace ZooMergeTest.Tests
{
    /// <summary>
    /// Тестирует перемещение блоков по горизонтали и вертикали внутри.
    /// </summary>
    [TestClass]
    public class BlockMoverTests
    {
        /// <summary>
        /// Проверяет, что блок может двигаться вправо до границы арены, если путь свободен.
        /// </summary>
        [TestMethod]
        public void MoveHorizontal_BlockMoves_WhenPathFree()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            container.Add(0, 0, 1);

            var mover = new BlockMover(container, arena.Object);

            // Act
            mover.MoveHorizontal(1);

            // Assert
            Assert.AreEqual(3, container.Blocks[0].XPosition);
        }

        /// <summary>
        /// Проверяет, что блок не двигается вправо, если справа есть другой блок.
        /// </summary>
        [TestMethod]
        public void MoveHorizontal_BlockDoesNotMove_WhenBlocked()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            container.Add(1, 0, 1);
            container.Add(2, 0, 1);

            var firstBlock = container.Blocks[0];
            var mover = new BlockMover(container, arena.Object);

            // Act
            mover.MoveHorizontal(1);

            // Assert
            Assert.AreEqual(1, firstBlock.XPosition);
        }

        /// <summary>
        /// Проверяет, что блок может двигаться вниз до границы арены, если путь свободен.
        /// </summary>
        [TestMethod]
        public void MoveVertical_BlockMoves_WhenPathFree()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            container.Add(0, 0, 1);

            var mover = new BlockMover(container, arena.Object);

            // Act
            mover.MoveVertical(1);

            // Assert
            Assert.AreEqual(3, container.Blocks[0].YPosition);
        }

        /// <summary>
        /// Проверяет, что блок не двигается вниз, если под ним есть другой блок.
        /// </summary>
        [TestMethod]
        public void MoveVertical_BlockDoesNotMove_WhenBlocked()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            container.Add(1, 0, 1);
            container.Add(1, 1, 1);

            var firstBlock = container.Blocks[0];
            var mover = new BlockMover(container, arena.Object);

            // Act
            mover.MoveVertical(1);

            // Assert
            Assert.AreEqual(0, firstBlock.YPosition);
        }
    }
}
