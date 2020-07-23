using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    /// <summary>
    /// An enum for defining the Robot Status after each Move or Place command.
    /// </summary>
    public enum RobotStatus
    {
        CorrectPlaceToStand,
        InvalidPositionForRobot,
    }
    //An abstract class as a base class for implementing shared functionality
    public abstract class Robot : IRobot
    {
        /// <summary>
        /// The board of the Robot as Surface to move.
        /// </summary>
        public IBoard Board { get; set; }
        /// <summary>
        ///  The Robot status after each move or Place.
        /// </summary>
        public RobotStatus RobotStatus { get; set; }
        /// <summary>
        ///  The Robot direction and Position
        /// </summary>
        public Route RobotRoute { get; set; } = null;

        //this boolean represents the validation of first Place command.
        public bool IsRobotPlacedAtFirstTime { get; set; } = false;

        /// <summary>
        /// Define the Board surface of the Robot.
        /// </summary>
        /// <param name="board">The Board object </param>
        public Robot(IBoard board)
        {
            Board = board;
        }
        /// <summary>
        /// Place the robot in the specific Route.
        /// It will check the valid place and changes the "RobotStatus" property
        /// </summary>
        /// <param name="route"> the route that the Robot should be placed </param>
        public void Place(Route route)
        {
            if ((route is null) ||
              (!Board.IsValidPosition(route.RobotPosition))) { RobotStatus = RobotStatus.InvalidPositionForRobot; return; }
            RobotRoute = route;
            RobotStatus = RobotStatus.CorrectPlaceToStand;
            IsRobotPlacedAtFirstTime = true;
        }
        //an abstarct move method for childs implementation
        public abstract void Move();

        //an abstarct Left method for childs implementation
        public abstract void TurnLeft();

        //an abstarct Right method for childs implementation
        public abstract void TurnRight();

        //an abstarct Report method for childs implementation
        public abstract string Report();

    }
}
