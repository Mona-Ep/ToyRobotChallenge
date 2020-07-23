using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It is the Report command which executes the Report method of Robot.
    //Report output stores in the Robot "LastReportOfRobot" property. 
    public class ReportCommand : Icommand
    {
        //used for keeping the last report of the Robot 
        public string LastReportOfRobot { get; set; }
        /// <summary>
        /// It executes the Report method of Robot.
        /// Report output stores in the Robot "LastReportOfRobot" property. 
        /// </summary>
        /// <param name="robot">The Robot object</param>
        public void Execute(IRobot robot)
        {
            LastReportOfRobot = robot.Report();
        }
    }
}
