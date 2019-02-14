using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maze : MonoBehaviour
{
    public IntVector2 size;

    public MazeCell cellPrefab;
    private MazeCell[,] cells;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
   
    //// Start is called before the first frame update
    //void Start()
    //{
       
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //Does the first step in generating a maze cell
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    //Complete the next steps in generating a maze
    //retrieves the current cell, check whether the move is possible
    //and removes cells from the list 
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activeCells.Add(CreateCell(coordinates));
        }
        else
        {
            activeCells.RemoveAt(currentIndex);
        }
    }


    //retrieves the maze's cell at some coordinates
    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    //generate method to construct the contents of the maze
    public float generationStepDelay;
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }


    //create a maze cell
    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    //Produces a random set of coordinates 
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    //ContainsCoordinates returns true if a set of coordinates falls inside the maze
    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;

    }

}
