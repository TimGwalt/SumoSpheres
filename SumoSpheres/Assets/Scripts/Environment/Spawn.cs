// This script is not being used in the final version of the game. It is only being saved for posterity.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] player;
    //public GameObject prefab1 = prefab;
    float maxlives = 3f;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = Resources.LoadAll("Prefabs/Characters") as GameObject[];
        SpawnIn();

    }

    private void SpawnIn()
    {
        prefab = player[i];
        Instantiate(prefab, transform.position, Quaternion.identity);
        i++;
    }

    
    private void lifeLoss(float num)
    {
        maxlives = num - 1;
    }


    // Update is called once per frame
    void Update()
    {
        while(maxlives > 0)
        {
            if (prefab.transform.position.y < -10)
            {
                lifeLoss(maxlives);
            }

        }
        Destroy(prefab);
    }
}
