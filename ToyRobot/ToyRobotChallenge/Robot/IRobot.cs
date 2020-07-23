using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //An interface for all Robots with basic functionality
    public interface IRobot
    {
        Route RobotRoute { get; set; }
        IBoard Board { get; set; }
        bool IsRobotPlacedAtFirstTime { get; set; }

        void Place(Route route);
        void Move();
        void TurnLeft();
        void TurnRight();
        string Report();

    }
}
