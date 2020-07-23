using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    /// <summary>
    /// Definition of the Direction
    /// </summary>
    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
    // Class for Route definition
    public class Route
    {
        /// <summary>
        /// position of The robot
        /// </summary>
        public Position RobotPosition { get; set; }
        /// <summary>
        /// Direction of the Robot
        /// </summary>
        public Direction RobotFaceDirection { get; set; }
        /// <summary>
        ///Route Definition of the robot
        /// </summary>
        /// <param name="position">position of The robot</param>
        /// <param name="direction">Direction of the Robot</param>
        public Route(Position position, Direction direction)
        {
            RobotPosition = position;
            RobotFaceDirection = direction;
        }
    }
}
