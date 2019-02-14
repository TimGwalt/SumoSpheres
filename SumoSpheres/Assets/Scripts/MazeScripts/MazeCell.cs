using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCell : MonoBehaviour
{
    public IntVector2 coordinates;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    //retrieves an edge from the mazeCell
    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    //Sets an edge in a maze cell
    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
    }


}
