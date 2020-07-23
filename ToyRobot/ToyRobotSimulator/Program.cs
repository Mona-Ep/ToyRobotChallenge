using System;
using ToyRobotChallenge;

namespace ToyRobotSimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run(args);

            // wait to see the result.
            Console.ReadLine();
        }
        public static void Run(string[] args)
        {
            //Toy Robot surface.
            Board board = new Board();
            //The Toy Robot
            ToyRobot toyRobot = new ToyRobot(board);
            //The simulator to execute Commands
            Simulator toyRobotSimulator = new Simulator(toyRobot);
            //Start to Play
            foreach (var arg in args)
            {
                toyRobotSimulator.Start(arg.ToString());
            }

        }
    }
}
