using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It is the Place command which executes the Place method of Robot.
    public class PlaceCommand : Icommand
    {
        //Place Route contains a Position and a direction of the Robot.
        private Route PlaceRoute { get; set; }

        /// <summary>
        /// Definition of Place Command
        /// </summary>
        /// <param name="route">Place Route contains a Position and a direction of the Robot.</param>
        public PlaceCommand(Route route)
        {
            PlaceRoute = route;
        }
        /// <summary>
        /// It executes the Place method of Robot.
        /// </summary>
        /// <param name="robot">The Robot object</param>
        public void Execute(IRobot robot)
        {
            robot.Place(PlaceRoute);
        }

    }
}
