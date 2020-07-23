using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyRobotChallenge
{
    //If any changes happened in rules, Const Values and Rules are defined in one class.

    public static class CommandRules
    {
        //The sample of commands :
        // PLACE X,Y, FACING => PLACE 0,0,NORTH
        // MOVE
        // LEFT
        // RIGHT
        // REPORT
        // PLACE will put the toy robot on the table in position X, Y and facing NORTH, SOUTH, EAST or WEST.
        //The origin(0,0) can be considered to be the SOUTH WEST most corner.
        //The first valid command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command. The application should discard all commands in the sequence until a valid PLACE command has been executed.
        //MOVE will move the toy robot one unit forward in the direction it is currently facing. LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.
        //REPORT will announce the X, Y and FACING of the robot. This can be in any form, but standard output is sufficient.
        //A robot that is not on the table can choose the ignore the MOVE, LEFT, RIGHT and REPORT commands.
        //Input can be from a file, or from standard input, as the developer chooses.

        /// <summary>
        /// Definition of the command Type
        /// </summary>
        public enum CommandTypes
        {
            PLACE,
            MOVE,
            LEFT,
            RIGHT,
            REPORT
        }
        //Regex pattern is used to fetch all possibility of commands.
        public static Regex CommandsPattern()
        {
            Regex commandPattern = new Regex(@"(((Place)\s*(\d+)\s*,\s*(\d+)\s*,\s*(NORTH|SOUTH|WEST|EAST)\s*)|((move|left|right|report)))",
                       RegexOptions.IgnoreCase,
                       TimeSpan.FromSeconds(1));
            return commandPattern;
        }
        //Regex pattern is used to fetch all possibility in the Place command.
        public static Regex PlaceCommandPattern()
        {
            Regex placePattern = new Regex(@"((Place)\s*(\d+)\s*,\s*(\d+)\s*,\s*(NORTH|SOUTH|WEST|EAST)\s*)",
                                  RegexOptions.IgnoreCase,
                                  TimeSpan.FromSeconds(1));
            return placePattern;
        }


    }
}
