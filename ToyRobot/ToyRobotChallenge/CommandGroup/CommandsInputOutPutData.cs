using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //It could be used for future feature to having all input and report result all together
    public class CommandsInputOutPutData
    {
        /// <summary>
        /// Command in string
        /// </summary>
        public string CommandAsString { get; set; }
        /// <summary>
        /// Command as object
        /// </summary>
        public Icommand CommandAsType { get; set; }
        /// <summary>
        /// Result of Report for beneficial output collection
        /// </summary>
        public string ReportOfRobot { get; set; }
    }
}
