using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Windows.Controls;
using ZooMerge;


namespace ZooMergeTest.Tests
{
    /// <summary>
    /// Тестирует добавление блоков в контейнер и занятых позиций.
    /// 
    /// AAA — это структура написания юнит‑теста, аббревиатура от:
    /// Arrange — подготовка тестового окружения;
    /// Act — выполнение тестируемого действия;
    /// Assert — проверка результата.
    /// 
    /// Описание в целом, можно будет удалить, для ознакомления больше.
    /// </summary>
    [TestClass]
    public class BlockContainerTests
    {
        /// <summary>
        /// Проверяет, что блок успешно добавляется в контейнер.
        /// </summary>
        [TestMethod]
        public void Add_Block_AddsSuccessfully()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            // Act
            container.Add(0, 0, 1);

            // Assert
            Assert.AreEqual(1, container.Blocks.Count);
            Assert.AreEqual(0, container.Blocks[0].XPosition);
            Assert.AreEqual(0, container.Blocks[0].YPosition);
            Assert.AreEqual(1, container.Blocks[0].Level);
        }

        /// <summary>
        /// Проверяет, что попытка добавить блок на уже занятую позицию вызывает исключение.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_Block_Throws_WhenPositionOccupied()
        {
            // Arrange
            var arena = new Mock<IReadArenaSize>();
            arena.Setup(a => a.SizeX).Returns(4);
            arena.Setup(a => a.SizeY).Returns(4);

            var grid = new Grid();
            var container = new BlockContainer(grid, arena.Object);

            container.Add(1, 1, 1);

            // Act
            container.Add(1, 1, 2);
        }
    }
}

