using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace ToyRobotChallenge
{
    //This class is the base parser and command generator.
    //It gives the list of string commands and has a generator function to 
    //generate a list of CommandInputOutput object which contains 
    //string type of commands, actual type of command and a string of report.
    public static class Commands
    {
        //this boolean represents the validation of first Place command.
        private static bool HasPlacedCommandbeenSeenFirst { get; set; } = false;

        /// <summary>
        /// This Factory method, converting the input string list to the 
        /// a list of CommandInputOutput object which contains 
        /// string type of commands, actual type of command and a string of report.
        /// the string part used for print input data.
        /// </summary>
        /// <param name="commandsStringList">list of string commands.Each line could have more than one command.</param>
        /// <returns>list of CommandInputOutput object</returns>
        public static List<CommandsInputOutPutData> GenerateOutputListOfCommands(List<string> commandsStringList)
        {
            //the output of function
            List<CommandsInputOutPutData> lstCommand = new List<CommandsInputOutPutData>();
            //this list will capture all index of Place commands
            List<int> AllPlaceCommandIndexes = new List<int>();

            // start index is from the first Place command in the list
            int startListIndex = 0;
            //this tag checks the first finded Place command
            HasPlacedCommandbeenSeenFirst = false;
            try
            {
                foreach (var fileLineCommands in commandsStringList)
                {
                    // this match is for fetching all valid command from the command Rules
                    // more than one command in line is acceptable
                    var matchedLineList = CommandRules.CommandsPattern().Matches(fileLineCommands)
                                        .Cast<Match>().Select(m => m.Value).ToList();

                    if (matchedLineList.Count == 0) continue;
                    //filling the list of all Place command Indexes for define the command in command generator
                    AllPlaceCommandIndexes = matchedLineList.Where(mat =>
                                                    CommandRules.PlaceCommandPattern().Match(mat).Success).
                                                    Select<string, int>(x => matchedLineList.IndexOf(x)).ToList();
                    if (!HasPlacedCommandbeenSeenFirst)
                    {
                        // if there is no Place command 
                        if (AllPlaceCommandIndexes.Count == 0) continue;
                        HasPlacedCommandbeenSeenFirst = true;
                        startListIndex = AllPlaceCommandIndexes[0];
                    }
                    else { startListIndex = 0; }
                    // this loop is for generating all commands in each line and adding it to the list
                    for (int i = startListIndex; i < matchedLineList.Count; i++)
                    {
                        var generatedCommand = CommandGenerator(matchedLineList[i], AllPlaceCommandIndexes.Contains(i));
                        if (generatedCommand == null) continue;
                        //this is the output object for beneficial output print
                        var outputData = new CommandsInputOutPutData()
                        {
                            CommandAsString = matchedLineList[i],
                            CommandAsType = generatedCommand,
                            ReportOfRobot = ""
                        };
                        lstCommand.Add(outputData);
                    }
                }

            }
            catch (Exception)
            {
                var outputData = new CommandsInputOutPutData()
                {
                    CommandAsString = "",
                    CommandAsType = null,
                    ReportOfRobot = " wrong Data to parse"
                };
                lstCommand.Add(outputData);
            }
            return lstCommand;
        }

        /// <summary>
        /// Converting string Command to Command object.
        ///Each line could have more than one command.
        /// </summary>
        /// <param name="command">string command.</param>
        /// <param name="isPlaceCommand">is command a place command</param>
        /// <returns></returns>
        private static Icommand CommandGenerator(string command, bool isPlaceCommand = false)
        {
            CommandRules.CommandTypes commandsEnum = isPlaceCommand ? CommandRules.CommandTypes.PLACE :
                                                    (CommandRules.CommandTypes)Enum.Parse(typeof(CommandRules.CommandTypes),
                                                    command.Trim().ToUpper());
            switch (commandsEnum)
            {
                case CommandRules.CommandTypes.PLACE:
                    return ParsePlaceCommand(command);
                case CommandRules.CommandTypes.MOVE:
                    return new MoveCommand();
                case CommandRules.CommandTypes.LEFT:
                    return new LeftCommand();
                case CommandRules.CommandTypes.RIGHT:
                    return new RightCommand();
                case CommandRules.CommandTypes.REPORT:
                    return new ReportCommand();
                default:
                    return null;

            }

        }
        //Place command is the only command which has arguments.
        // This method checks the Place string command validation.
        //the out put is the Place command object with its arguments.
        private static Icommand ParsePlaceCommand(string command)
        {
            // PLACE X,Y, FACING Direction
            Regex placePattern = CommandRules.PlaceCommandPattern();
            try
            {
                var Matchedpatter = placePattern.Match(command);
                if (!Matchedpatter.Success) return null;

                int x = Convert.ToInt32(Matchedpatter.Groups[3].ToString());
                int y = Convert.ToInt32(Matchedpatter.Groups[4].ToString());

                Position routePosition = new Position(x, y);
                Direction direction = (Direction)Enum.Parse(typeof(Direction), Matchedpatter.Groups[5].ToString().ToUpper());

                Route placeRoute = new Route(routePosition, direction);
                Icommand placeCommand = new PlaceCommand(placeRoute);

                return placeCommand;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
