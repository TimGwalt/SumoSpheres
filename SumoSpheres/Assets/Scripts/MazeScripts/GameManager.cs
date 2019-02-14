using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        //begin game when start() is called

        BeginGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        //restart the game whenever "r" is pressed
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            RestartGame();
        }

    }

    public Maze mazePrefab;//reference to the prefab maze so we can create instances
    private Maze mazeInstance;//instance of maze 

    //to begin a game need to create a maze
    private void BeginGame() 
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }

    //destroy maze when game terminates and a new one begins
    private void RestartGame() 
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}
