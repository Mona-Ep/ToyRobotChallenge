using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotChallenge
{
    // Board class for Robot, with default width x height (5x5)
    //In this challenge board width & height is (5x5)
    public class Board : IBoard
    {
        public int Height { get; } = 5;
        public int Width { get; } = 5;
        /// <summary>
        /// Default height and Width is 5.
        /// The Board will be (5x5)
        /// </summary>
        public Board()
        {
        }
        /// <summary>
        /// Size of the board is defined with creating an instance.
        /// </summary>
        /// <param name="height"> Height of board </param>
        /// <param name="width"> Width of board</param>
        public Board(int height, int width)
        {
            //checking for negative value
            var boadValidHeightWidth = CheckForValidBoard(height, width);
            Height = boadValidHeightWidth.Item1;// true or Default value
            Width = boadValidHeightWidth.Item2;// true or Default value
        }
        /// <summary>
        /// It chackes if the Position is Positive and inside The Board.
        /// </summary>
        /// <param name="position">X and Y as position</param>
        /// <returns>Is it valid position</returns>
        public bool IsValidPosition(Position position)
        {
            return ((position.X >= 0 && position.Y >= 0) &&
                   (position.X <= Height && position.Y <= Width));
        }

        // If the height or width are negative it will 
        // changed them to default value. The default value is 5
        private Tuple<int, int> CheckForValidBoard(int height, int width)
        {
            return Tuple.Create((height < 0) ? 5 : height, (width < 0) ? 5 : width);
        }
    }
}
