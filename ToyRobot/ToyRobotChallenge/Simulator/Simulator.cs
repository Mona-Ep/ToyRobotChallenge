using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ToyRobotChallenge
{
    //This class is responsible for getting the Input file and calling the Execute Command
    public class Simulator
    {
        //The Toy Robot
        private IRobot SimulatorRobot { get; set; }
        /// <summary>
        /// The Toy Robot defines with simulator constructor.
        /// </summary>
        /// <param name="robot">The Toy Robot</param>
        public Simulator(IRobot robot)
        {
            SimulatorRobot = robot;
        }
        /// <summary>
        /// Execute each command in list of commands and print input and output data.
        /// </summary>
        /// <param name="lstCommand">list of CommandsInputOutPutData object </param>
        public void Execute(List<CommandsInputOutPutData> lstCommand)
        {
            //for each list of execution the Place command should be the first command
            SimulatorRobot.IsRobotPlacedAtFirstTime = false;
            foreach (var command in lstCommand)
            {
                Console.WriteLine(command.CommandAsString);
                command.CommandAsType.Execute(SimulatorRobot);
                // Result of Report for beneficial output collection
                //It could be used for future feature to having all input and report result all together
                command.ReportOfRobot = (command.CommandAsType.GetType() == (new ReportCommand()).GetType()) ?
                                          ((ReportCommand)command.CommandAsType).LastReportOfRobot : "";
                if (command.ReportOfRobot != "")
                {
                    Console.WriteLine(command.ReportOfRobot);
                    Console.WriteLine();
                }
            }

            if (!SimulatorRobot.IsRobotPlacedAtFirstTime)
                Console.WriteLine("There wasn't any correct command in the List.");
            else
                Console.WriteLine("All commands checked and executed.");
            
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine();
        }
        /// <summary>
        /// Start to Play with file name or filename and path 
        /// Filename if its near the Exe, Filename and path if file is not near the exe.
        /// </summary>
        /// <param name="filePathAndName"> Filename if its near the Exe, Filename and path if file is not near the exe. </param>
        public void Start(string filePathAndName)
        {
            try
            {
                if (!File.Exists(filePathAndName))
                {
                    filePathAndName = AppDomain.CurrentDomain.BaseDirectory + "\\" + filePathAndName;
                    if (!File.Exists(filePathAndName))
                    {
                        Console.WriteLine("Wrong File Path Or Name ");
                        return;
                    }
                }
                var listStringCommands = File.ReadLines(filePathAndName).ToList<string>();
                var listOfOutputCommands = Commands.GenerateOutputListOfCommands(listStringCommands);

                string[] filenameAndPathList = filePathAndName.Split('\\');
                string fileName = filenameAndPathList[filenameAndPathList.Length - 1];
                Console.WriteLine("========= {0}  File Started  =========", filePathAndName);

                Execute(listOfOutputCommands);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong File Data ");

            }
        }
    }


}
