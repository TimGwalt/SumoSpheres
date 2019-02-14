using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of the connections between cells, each cell has 
//four edges, each of which connects to a neighboring cell, it would lead
public abstract class MazeCellEdge : MonoBehaviour
{
    public MazeCell cell, otherCell;

    public MazeDirection direction;

    public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
    }

}
