using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It is the Move command which executes the Move method of Robot.
    public class MoveCommand : Icommand
    {
        /// <summary>
        /// It executes the Move method of Robot.
        /// </summary>
        /// <param name="robot">The Robot object</param>
        public void Execute(IRobot robot)
        {
            robot.Move();
        }
    }
}
