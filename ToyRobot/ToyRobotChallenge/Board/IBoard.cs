using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotChallenge
{
    //An interface for all boards with basic functionality
    public interface IBoard
    {
        int Height { get; }
        int Width { get; }
        bool IsValidPosition(Position position);

    }
}
