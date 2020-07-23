using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //The ToyRobot Challenge Robot class
    // functions override through the ToyRobot Rules 
    public class ToyRobot : Robot
    {
       //It follows the Robot Base Class
        public ToyRobot(IBoard board) : base(board)
        {
        }
        /// <summary>
        /// MOVE will move the toy robot one unit forward in the direction it is currently facing. 
        /// </summary>
        public override void Move()
        {
            var previousPosition = new Position(RobotRoute.RobotPosition.X, RobotRoute.RobotPosition.Y);
            if (RobotRoute.RobotFaceDirection == Direction.EAST) RobotRoute.RobotPosition.X++;
            if (RobotRoute.RobotFaceDirection == Direction.WEST) RobotRoute.RobotPosition.X--;
            if (RobotRoute.RobotFaceDirection == Direction.NORTH) RobotRoute.RobotPosition.Y++;
            if (RobotRoute.RobotFaceDirection == Direction.SOUTH) RobotRoute.RobotPosition.Y--;
            if (!Board.IsValidPosition(RobotRoute.RobotPosition))
            {
                RobotRoute.RobotPosition = previousPosition;
                RobotStatus = RobotStatus.InvalidPositionForRobot;
                return;
            }
            RobotStatus = RobotStatus.CorrectPlaceToStand;
        }
        /// <summary>
        /// REPORT will announce the X,Y and FACING of the robot. like : Output: 0,1,NORTH
        /// </summary>
        /// <returns></returns>
        public override string Report()

        {
            if (RobotRoute is null) return "";

            return string.Format("OutPuT: {0},{1}, {2}",
                                 RobotRoute.RobotPosition.X,
                                 RobotRoute.RobotPosition.Y,
                                 RobotRoute.RobotFaceDirection);
        }

        /// <summary>
        ///  LEFT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.
        /// </summary>
        public override void TurnLeft()
        {
            switch (RobotRoute.RobotFaceDirection)
            {
                case Direction.NORTH:
                    RobotRoute.RobotFaceDirection = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    RobotRoute.RobotFaceDirection = Direction.EAST;
                    break;
                case Direction.EAST:
                    RobotRoute.RobotFaceDirection = Direction.NORTH;
                    break;
                case Direction.WEST:
                    RobotRoute.RobotFaceDirection = Direction.SOUTH;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        ///  Right will rotate the robot 90 degrees in the specified direction without changing the position of the robot.
        /// </summary>
        public override void TurnRight()
        {
            switch (RobotRoute.RobotFaceDirection)
            {
                case Direction.NORTH:
                    RobotRoute.RobotFaceDirection = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    RobotRoute.RobotFaceDirection = Direction.WEST;
                    break;
                case Direction.EAST:
                    RobotRoute.RobotFaceDirection = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    RobotRoute.RobotFaceDirection = Direction.NORTH;
                    break;
                default:
                    break;
            }
        }
    }
}
