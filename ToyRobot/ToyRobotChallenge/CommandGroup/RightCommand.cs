using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It is the Right command which executes the Right method of Robot.
    public class RightCommand : Icommand
    {
        /// <summary>
        /// It executes the TurnRight method of Robot.
        /// </summary>
        /// <param name="robot">The Robot object</param>
        public void Execute(IRobot robot)
        {
            robot.TurnRight();
        }
    }
}
