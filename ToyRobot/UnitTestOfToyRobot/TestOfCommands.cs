using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotChallenge;

namespace UnitTestOfToyRobot
{
    #region "Test of each Command is working Properly"

    [TestClass]
    public class TestOfCommands
    {
        private ToyRobot PlaceRobotInPosition(Position position, Direction direction)
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            Route route = new Route(position, direction);
            PlaceCommand placeCommand = new PlaceCommand(route);
            placeCommand.Execute(robot);
            return robot;
        }
        [TestMethod]
        public void PlaceRobotInCorrectPositionWithPlaceCommandTheRobotIsPlacedCorrectly()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            Route route = new Route(new Position(1, 2), Direction.NORTH);
            PlaceCommand placeCommand = new PlaceCommand(route);
            placeCommand.Execute(robot);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
        }
        [TestMethod]
        public void TheRobotMovementWithMoveCommandCorrectXPositionToNorth()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 1), Direction.NORTH);
            MoveCommand moveCommand = new MoveCommand();
            moveCommand.Execute(robot);

            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            Assert.AreEqual(robot.RobotRoute.RobotPosition.Y, 2);

            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
        }
        [TestMethod]
        public void TheRobotShouldTurnAllDirectionWithTurnCommandAfterPlacedCorrectlly()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(3, 3), Direction.NORTH);
            LeftCommand leftCommand = new LeftCommand();
            RightCommand rightCommand = new RightCommand();
            //Place Robot At the edge of NORTH, face to NORTH
            leftCommand.Execute(robot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.WEST);

            rightCommand.Execute(robot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.NORTH);

            rightCommand.Execute(robot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.EAST);

            rightCommand.Execute(robot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.SOUTH);
        }

        [TestMethod]
        public void IfRobotPlaceCorrectlyTheReportCommandAnswerTheCorrectPosition()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 2), Direction.NORTH);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);

            ReportCommand reportCommand = new ReportCommand();
            //The report should be  "OutPuT: 1,2, NORTH"
            reportCommand.Execute(robot);
            Assert.AreEqual(reportCommand.LastReportOfRobot, "OutPuT: 1,2, NORTH");
            Console.WriteLine(reportCommand.LastReportOfRobot);
        }
        [TestMethod]
        public void IfRobotPlacedNotCorrectlyAtFirstTimeReportCommandAnsweredIsNotPlacedProperly()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(10, 2), Direction.NORTH);
            Assert.IsNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);
            ReportCommand reportCommand = new ReportCommand();
            //The report should be empty
            reportCommand.Execute(robot);
            Assert.AreEqual(reportCommand.LastReportOfRobot, "");
            Console.WriteLine(reportCommand.LastReportOfRobot);
        }
        [TestMethod]
        public void IfRobotPlacedCorrectlyAtFirstTimeButNotCorrectlyAfterReportShouldShowLastPosition()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 2), Direction.NORTH);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);

            MoveCommand moveCommand = new MoveCommand();
            moveCommand.Execute(robot);

            PlaceCommand placeCommand = new PlaceCommand(new Route(new Position(10, 2), Direction.NORTH));
            //This wrong Place will not set
            placeCommand.Execute(robot);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);

            ReportCommand reportCommand = new ReportCommand();
            //The report should be Last Position "OutPuT: 1,3, NORTH"
            reportCommand.Execute(robot);
            Assert.AreEqual(reportCommand.LastReportOfRobot, "OutPuT: 1,3, NORTH");
            Console.WriteLine(reportCommand.LastReportOfRobot);
            moveCommand.Execute(robot);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            reportCommand.Execute(robot);
            Assert.AreEqual(reportCommand.LastReportOfRobot, "OutPuT: 1,4, NORTH");
            Console.WriteLine(reportCommand.LastReportOfRobot);
        }
        #endregion

        #region "Test of Generating each Command from string"

        [TestMethod]
        public void CorrectStringOfPlaceCommandShouldReturnPlaceCommand()
        {
            var placeCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 4,2, West" });
            PlaceCommand actualPlaceCommand = new PlaceCommand(new Route(new Position(4, 2), Direction.WEST));
            Assert.IsTrue(placeCommandInList[0].CommandAsType.GetType().Equals(actualPlaceCommand.GetType()));
        }
        [TestMethod]
        public void WrongStringOfPlaceCommandShouldReturnNull()
        {
            var placeCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 7,2 West" });
            Assert.IsTrue(placeCommandInList.Count == 0);
        }
        [TestMethod]
        public void CorrectStringOfMoveCommandShouldReturnMoveCommand()
        {
            var moveCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 0,0, east Move" });
            MoveCommand actualMoveCommand = new MoveCommand();
            Assert.IsTrue(moveCommandInList[1].CommandAsType.GetType().Equals(actualMoveCommand.GetType()));
        }
        [TestMethod]
        public void WrongStringOfMoveCommandShouldReturnNull()
        {
            var moveCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "23 Move" });
            Assert.IsTrue(moveCommandInList.Count == 0);
        }
        [TestMethod]
        public void CorrectStringOfLeftAndRightCommandShouldReturnLeftAndRightCommand()
        {
            var leftCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 2,3, east Left" });
            LeftCommand actualLeftCommand = new LeftCommand();
            Assert.IsTrue(leftCommandInList[1].CommandAsType.GetType().Equals(actualLeftCommand.GetType()));

            var rightCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 2,3, east right" });
            RightCommand actualRightCommand = new RightCommand();
            Assert.IsTrue(rightCommandInList[1].CommandAsType.GetType().Equals(actualRightCommand.GetType()));
        }
        [TestMethod]
        public void WrongStringOfLeftAndRightCommandShouldReturnNull()
        {
            var leftCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "go Left" });
            Assert.IsTrue(leftCommandInList.Count == 0);

            var rightCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "turn right" });
            Assert.IsTrue(rightCommandInList.Count == 0);
        }
        [TestMethod]
        public void CorrectStringOfReportCommandShouldReturnReportCommand()
        {
            var reportCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "PLACE 2,4, South report" });
            ReportCommand actualReportCommand = new ReportCommand();
            Assert.IsTrue(reportCommandInList[1].CommandAsType.GetType().Equals(actualReportCommand.GetType()));
        }
        [TestMethod]
        public void WrongStringOfReportCommandShouldReturnReportCommand()
        {
            var reportCommandInList = Commands.GenerateOutputListOfCommands(new List<string>() { "now Report" });
            Assert.IsTrue(reportCommandInList.Count == 0);
        }
        [TestMethod]
        public void CorrectStringListOfCommandWouldGenerateCorrectListOfCommand()
        {
            List<string> stringCommands = new List<string>()
            {
                "PLACE 0,0, East Move",
                "left left Move ",
                "right right Move",
                "Left  PLACE 0,0, East",
                "Move",
                "right right",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.AreEqual(listOfCommands.Count, 14);
        }
        [TestMethod]
        public void AllCommandBeforeFisrtPlaceShouldBeIgnored()
        {
            List<string> stringCommands = new List<string>()
            {
                "Move",
                "Move",
                "Left",
                "Move PLACE 0,0, East",
                "Move",
                "right",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.AreEqual(listOfCommands.Count, 4);
        }

        [TestMethod]
        public void AllInvalidStringsShouldBeIgnored()
        {
            List<string> stringCommands = new List<string>()
            {
                "Move",
                "Left PLACE 0,0, East",
                "Turn Left",
                "Now stop",
                "Move ",
                "PLACE 0,0West",
                "move right",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.AreEqual(listOfCommands.Count, 6);
        }
        #endregion

        #region" Test of executing List of commands"
        [TestMethod]
        public void CorrectListOfCommandWouldReturnCorrectOutput()
        {
            List<string> stringCommands = new List<string>()
            {
                "PLACE 0,0, North move ",
                "Move",
                "right",
                "Move",
                "left",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.IsNotNull(listOfCommands);
            Assert.AreEqual(listOfCommands.Count, 7);

            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            foreach (var command in listOfCommands)
            {
                command.CommandAsType.Execute(robot);
                command.ReportOfRobot = (command.CommandAsType.GetType() == (new ReportCommand()).GetType()) ?
                                      ((ReportCommand)command.CommandAsType).LastReportOfRobot : "";
                Assert.IsNotNull(robot.RobotRoute);
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            }
            // report should be "OutPuT: 1,1, NORTH"
            Assert.AreEqual(listOfCommands[listOfCommands.Count - 1].ReportOfRobot, "OutPuT: 1,2, NORTH");
        }
        [TestMethod]
        public void WrongCommandWouldBeIgnored()
        {
            List<string> stringCommands = new List<string>()
            {
                "Move",
                "PLACE 0,0 East",
                "Turn Left",
                "Now stop",
                "PLACE 0,0, East Move",
                "PLACE 0,0West",
                "move right",
                "Report",
            };

            var listOfCommands = Commands.GenerateOutputListOfCommands(stringCommands);

            Assert.IsNotNull(listOfCommands);
            Assert.AreEqual(listOfCommands.Count, 5);

            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            foreach (var command in listOfCommands)
            {
                command.CommandAsType.Execute(robot);
                command.ReportOfRobot = (command.CommandAsType.GetType() == (new ReportCommand()).GetType()) ?
                                      ((ReportCommand)command.CommandAsType).LastReportOfRobot : "";
                Assert.IsNotNull(robot.RobotRoute);
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                // report should console writeline "OutPuT: 2,0, EAST"
            }
            // report should be "OutPuT: 2,0, EAST"
            Assert.AreEqual(listOfCommands[listOfCommands.Count - 1].ReportOfRobot, "OutPuT: 2,0, SOUTH");
        }
        #endregion
    }

}

