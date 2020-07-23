using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotChallenge;
namespace UnitTestOfToyRobot
{
    [TestClass]
    public class TestOfBoardAndPositions
    {
        [TestMethod]
        public void BoardShouldbeCreated()
        {
            Board boardWithNegativeHeightAndWidth = new Board(-1, -5);
            Board boardWithDefaultHeightAndWidth = new Board();//Deafult Board is (5,5)  

            Assert.IsNotNull(boardWithNegativeHeightAndWidth);
            Assert.IsNotNull(boardWithDefaultHeightAndWidth);
        }

        [TestMethod]
        public void SetDefaultNumberToBoardIfHieghtOrWidthIsNegative()
        {
            Board boardWithNegativeHeightAndWidth = new Board(-1, -5);
            Board boardWithDefaultHeightAndWidth = new Board();//Deafult Board is (5,5)  

            Assert.AreEqual(boardWithNegativeHeightAndWidth.Height, boardWithDefaultHeightAndWidth.Height);
            Assert.AreEqual(boardWithNegativeHeightAndWidth.Width, boardWithDefaultHeightAndWidth.Width);
        }

        [TestMethod]
        public void CheckAllValidPositionsInBoard()
        {
            Board boardWithDefaultHeightAndWidth = new Board();//Deafult Board is (5,5)  
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 0; y <= 5; y++)
                {
                    Assert.IsTrue(boardWithDefaultHeightAndWidth.IsValidPosition(new Position(x, y)));
                }
            }
        }


        [TestMethod]
        public void TheIsValidPositionReturnFalseIfPositionOutOfRange()
        {
            Board board = new Board();//Deafult Board is (5,5)       
            Assert.IsFalse(board.IsValidPosition(new Position(board.Height + 1, board.Width + 10)));
        }

    }
}
