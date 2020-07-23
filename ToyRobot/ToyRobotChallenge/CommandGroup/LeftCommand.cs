using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It is the Left command which executes the TurnLeft method of Robot.
    public class LeftCommand : Icommand
    {
        /// <summary>
        /// It executes the TurnLeft method of Robot.
        /// </summary>
        /// <param name="robot">The Robot object</param>
        public void Execute(IRobot robot)
        {
            robot.TurnLeft();
        }
    }
}
