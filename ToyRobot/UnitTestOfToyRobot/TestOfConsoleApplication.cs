using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestOfToyRobot
{
    [TestClass]
    public class TestOfConsoleApplication
    {
        [TestMethod]
        public void RunTestCorrectCommandsFileShouldReportProperly()
        {
            string[] args = new string[1];
            args[0] = "TestCorrectCommands.txt";
            ToyRobotSimulator.Program.Run(args);
            //List Of executed commands should print in output
        }
        [TestMethod]
        public void RunTestCorrectCommandsWithOutReportCommandFileShouldReportProperly()
        {
            string[] args = new string[1];
            args[0] = "TestCorrectCommandsWithOutReportCommand.txt";
            ToyRobotSimulator.Program.Run(args);
            //List Of executed commands without any Report result should print in output
        }

        [TestMethod]
        public void RunTestNotValidCommandsFileShouldReportProperly()
        {
            string[] args = new string[1];
            args[0] = "TestNotValidCommands.txt";
            ToyRobotSimulator.Program.Run(args);
            //output print : "There wasn't any correct command in the List. For exit just press a key."
           

        }

        [TestMethod]
        public void RunTestValidAndNotValidCommandsFileShouldReportProperly()
        {
            string[] args = new string[1];
            args[0] = "TestValidAndNotValidCommands.txt";
            ToyRobotSimulator.Program.Run(args);
            //List Of executed commands should print in output
        }
        [TestMethod]
        public void RunFourTestCommandsFileShouldReportProperly()
        {
            string[] args = new string[4];
            args[0] = "TestCorrectCommands.txt";
            args[1] = "TestValidAndNotValidCommands.txt";
            args[2] = "TestCorrectCommandsWithOutReportCommand.txt";
            args[3] = "TestNotValidCommands.txt";
            ToyRobotSimulator.Program.Run(args);
            //List Of executed commands should print in output
        }
    }
}
