using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotChallenge
{
    //Definition of position of the Robot
    public class Position
    {
        //the vertical position of the robot
        public int X { get; set; }
        //the horizontal position of the robot
        public int Y { get; set; }

        //Definition of position of the Robot
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
