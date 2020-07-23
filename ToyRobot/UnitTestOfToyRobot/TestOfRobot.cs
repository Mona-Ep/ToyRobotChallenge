using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotChallenge;

namespace UnitTestOfToyRobot
{
    [TestClass]
    public class TestOfRobot
    {

        public ToyRobot PlaceRobotInPosition(Position position, Direction direction)
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            robot.Place(new Route(position, direction));
            return robot;
        }        
        [TestMethod]
        public void PlaceRobotInCorrectPositionTheRobotIsPlacedCorrectly()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 2), Direction.NORTH);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
        }
        [TestMethod]
        public void PlaceRobotInNotCorrectPositionTheRobotShouldntPlaced()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(10, 2), Direction.NORTH);
            Assert.IsNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);
        }
        [TestMethod]
        public void IfRobotPlaceCorrectlyTheReportAnswerTheCorrectPosition()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 2), Direction.NORTH);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            //The report should be "OutPuT: 1,2, NORTH"
            Assert.AreEqual(robot.Report(), "OutPuT: 1,2, NORTH");
            Console.WriteLine(robot.Report());
        }
        [TestMethod]
        public void IfRobotNotPlacedCorrectlyAtFirstTimeReportAnsweredIsNotPlacedProperly()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(10, 2), Direction.NORTH);
            Assert.IsNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);
            //The report should be empty
            Assert.AreEqual(robot.Report(), "");
            Console.WriteLine(robot.Report());
        }
        [TestMethod]
        public void IfRobotPlacedCorrectlyAtFirstTimeButNotCorrectlyAfterReportShouldShowLastPosition()
        {
            ToyRobot robot = PlaceRobotInPosition(new Position(1, 2), Direction.NORTH);
            Assert.IsNotNull(robot.RobotRoute);
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            robot.Move();
            //This wrong Place will not set
            robot.Place(new Route(new Position(10, 2), Direction.NORTH));
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);

            //The report should be Last Position "OutPuT: 1,3, NORTH"
            Assert.AreEqual(robot.Report(), "OutPuT: 1,3, NORTH");
            Console.WriteLine(robot.Report());
            robot.Move();
            Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            Assert.AreEqual(robot.Report(), "OutPuT: 1,4, NORTH");
            Console.WriteLine(robot.Report());
        }
        [TestMethod]
        public void TheRobotMovementFromAllCorrectXPositionToNorth()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            for (int x = 0; x <= 5; x++)
            {
                //Place the Robot on button SOUTH, Face to NORTH
                robot.Place(new Route(new Position(x, 0), Direction.NORTH));
                for (int y = 1; y <= 5; y++)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                    Assert.AreEqual(robot.RobotRoute.RobotPosition.Y, y);
                }
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            }
        }
        [TestMethod]
        public void TheRobotMovementFromAllCorrectXPositionToSouth()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            for (int x = 0; x <= 5; x++)
            {
                //Place the Robot on Top NORTH, Face to SOUTH
                robot.Place(new Route(new Position(x, 5), Direction.SOUTH));
                for (int y = 4; y >= 0; y--)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                    Assert.AreEqual(robot.RobotRoute.RobotPosition.Y, y);
                }
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            }
        }
        [TestMethod]
        public void TheRobotMovementFromAllCorrectYPositionToEast()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            for (int y = 0; y <= 5; y++)
            {
                //Place the Robot on Left WEST, Face to EAST
                robot.Place(new Route(new Position(0, y), Direction.EAST));
                for (int x = 1; x <= 5; x++)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                    Assert.AreEqual(robot.RobotRoute.RobotPosition.X, x);
                }
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            }
        }
        [TestMethod]
        public void TheRobotMovementFromAllCorrectYPositionToWest()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            for (int y = 0; y <= 5; y++)
            {
                //Place the Robot on Right EAST, Face to WEST
                robot.Place(new Route(new Position(5, y), Direction.WEST));
                for (int x = 4; x >= 0; x--)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                    Assert.AreEqual(robot.RobotRoute.RobotPosition.X, x);
                }
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
            }
        }

        [TestMethod]
        public void TheRobotMovementToWrongXPositionWillBeIgnored()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5
            //Place Robot At the edge of east, face to EAST
            robot.Place(new Route(new Position(5, 0), Direction.EAST));
            robot.Move();
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotPosition.X, 5);
        }

        [TestMethod]
        public void TheRobotMovementToWrongYPositionWillBeIgnored()
        {
            Board board = new Board();//Default Borad is 5 x 5
            ToyRobot robot = new ToyRobot(board);
            //Place Robot At the edge of NORTH, face to NORTH
            robot.Place(new Route(new Position(0, 5), Direction.NORTH));
            robot.Move();
            Assert.AreEqual(robot.RobotStatus, RobotStatus.InvalidPositionForRobot);
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotPosition.Y, 5);

            Assert.IsTrue(board.IsValidPosition(robot.RobotRoute.RobotPosition));
        }
        [TestMethod]
        public void TheRobotShouldTurnAllDirectionAfterPlacedCorrectlly()
        {
             ToyRobot robot = PlaceRobotInPosition(new Position(3, 3), Direction.NORTH);
            //Place Robot At the edge of NORTH, face to NORTH
            robot.TurnLeft();
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.WEST);

            robot.TurnRight();
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.NORTH);

            robot.TurnRight();
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.EAST);

            robot.TurnRight();
            //Robot stayed at the last correct position
            Assert.AreEqual(robot.RobotRoute.RobotFaceDirection, Direction.SOUTH);
        }
        [TestMethod]
        public void TheRobotCouldWalkThroughTheBoardSurfaceFromEastToWestAndReturnAndReportLastPosition()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            //walk from start position 0,0 face to east, move to the edge of EAST
            //then turn left and one move up,
            // agian turn left to face back WEST and move to the edge of WEST
            // continue this pattern to seek all the board surface to position 0,5
            robot.Place(new Route(new Position(0, 0), Direction.EAST));

            for (int verticalStep = 0; verticalStep <= 5; verticalStep++)
            {
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                for (int horizontalStep = 1; horizontalStep <= 5; horizontalStep++)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                }

                if (robot.RobotRoute.RobotFaceDirection == Direction.EAST)
                { robot.TurnLeft(); robot.Move(); robot.TurnLeft(); continue; }
                if (robot.RobotRoute.RobotFaceDirection == Direction.WEST)
                { robot.TurnRight(); robot.Move(); robot.TurnRight(); }
            }
            // The report should be "OutPuT: 0,5, EAST"
            Assert.AreEqual(robot.Report(), "OutPuT: 0,5, EAST");
            Console.WriteLine(robot.Report());
        }
        [TestMethod]
        public void TheRobotCouldWalkThroughTheBoardSurfaceFromSouthToNorthAndReturnAndReportLastPosition()
        {
            ToyRobot robot = new ToyRobot(new Board());//Default Borad is 5 x 5

            //walk from start position 0,0 face to NORTH, move to the edge of NORTH
            //then turn Right and one move right,
            // agian turn Right to face back SOUTH and move to the edge of SOUTH
            // continue this pattern to seek all the board surface to position 5,0
            robot.Place(new Route(new Position(0, 0), Direction.NORTH));

            for (int horizontalStep = 0; horizontalStep <= 5; horizontalStep++)
            {
                Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                for (int verticalStep = 1; verticalStep <= 5; verticalStep++)
                {
                    robot.Move();
                    Assert.AreEqual(robot.RobotStatus, RobotStatus.CorrectPlaceToStand);
                }

                if (robot.RobotRoute.RobotFaceDirection == Direction.NORTH)
                { robot.TurnRight(); robot.Move(); robot.TurnRight(); continue; }
                if (robot.RobotRoute.RobotFaceDirection == Direction.SOUTH)
                { robot.TurnLeft(); robot.Move(); robot.TurnLeft(); }
            }
            // the report should be "OutPuT: 5,0, NORTH"
            Assert.AreEqual(robot.Report(), "OutPuT: 5,0, NORTH");
            Console.WriteLine(robot.Report());
        }
    }
}
