using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//give a sense of direction when creating cells
public enum MazeDirection
{
    North,
    East,
    South,
    West
}

//static class that specializes in getting random directions to use when creating cells
public static class MazeDirections
{
    public const int Count = 4;

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    //static array of vectors to make conversion of direction to vector easy
    private static IntVector2[] vectors =
    {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0)
    };

    //converts a direction into an integer vector
    public static IntVector2 ToIntVector2(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
}