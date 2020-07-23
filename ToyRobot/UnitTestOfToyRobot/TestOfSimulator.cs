using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotChallenge;

namespace UnitTestOfToyRobot
{
    [TestClass]
    public class TestOfSimulator
    {


        [TestMethod]
        public void CreatingSimulatorBoardAndRobotAllOfThemShouldbeValid()
        {
            Board board = new Board();//Default Borad is 5 x 5
            ToyRobot robot = new ToyRobot(board);
            Simulator simulator = new Simulator(robot);
            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);
            Assert.IsNotNull(simulator);
        }


        [TestMethod]
        public void CorrectListOfCommandWouldReturnCorrectOutput()
        {
            List<string> stringCommands = new List<string>()
            {
                "PLACE 0,0, North",
                "Move",
                "right PLACE 1,1, North right",
                "Move Move ",
                "left",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.IsNotNull(listOfCommands);
            Assert.AreEqual(listOfCommands.Count, 9);

            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            Simulator simulator = new Simulator(robot);
            //All command with Report Result should be written in console
            simulator.Execute(listOfCommands);
            // report should be "OutPuT: 1,1, NORTH"
            Assert.AreEqual(listOfCommands[listOfCommands.Count - 1].ReportOfRobot, "OutPuT: 3,1, NORTH");
        }
        [TestMethod]
        public void WrongCommandWouldBeIgnored()
        {
            List<string> stringCommands = new List<string>()
            {
                "Move",
                "PLACE 0,0, East  PLACE 0,20, East",
                "Turn Left",
                "Now stop",
                "Move",
                "PLACE 0,0West",
                "move right",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.IsNotNull(listOfCommands);
            Assert.AreEqual(listOfCommands.Count, 7);

            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            Simulator simulator = new Simulator(robot);
            //All command with Report Result should be written in console
            simulator.Execute(listOfCommands);

            // report should be "OutPuT: 2,0, EAST"
            Assert.AreEqual(listOfCommands[listOfCommands.Count - 1].ReportOfRobot, "OutPuT: 0,2, EAST");
        }

        [TestMethod]
        public void CorrectFileCommandWouldReturnCorrectOutput()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            Simulator simulator = new Simulator(robot);
            string fileNameAndPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestCorrectCommands.txt";

            //All command with Report Result should be written in console
            simulator.Start(fileNameAndPath);

        }

        [TestMethod]
        public void InvalidCommandShouldBeIgnoredFromFileCommand()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            Simulator simulator = new Simulator(robot);
            string fileNameAndPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestValidAndNotValidCommands.txt";

            //All command with Report Result should be written in console
            simulator.Start(fileNameAndPath);

        }

        [TestMethod]
        public void MoreThanOneInputFileCommandShouldBeAccepted()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            Simulator simulator = new Simulator(robot);
            string fileNameAndPath = AppDomain.CurrentDomain.BaseDirectory +
                                    "\\TestCorrectCommands.txt" +
                                    "\\TestCorrectCommandsWithOutReportCommand.txt" +
                                    "\\TestNotValidCommands.txt" +
                                    "\\TestValidAndNotValidCommands.txt";

            //All command with Report Result should be written in console
            simulator.Start(fileNameAndPath);

        }
    }
}
