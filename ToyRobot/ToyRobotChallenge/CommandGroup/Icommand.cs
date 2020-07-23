using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //An interface for all commands with basic function.
    public interface Icommand
    {
        void Execute(IRobot robot);
    }
}
