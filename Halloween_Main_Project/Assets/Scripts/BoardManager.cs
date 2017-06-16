using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;


public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        //public constructor
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    //sizeboard
    public int columns = 8;
    public int rows = 8;

    public Count wallCount = new Count(5, 9); //how many walls do we want per level
    public Count foodCount = new Count(1, 5);//how many foods do we want per level

    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    /*to keep track of all the available positions on our grd
    and to checkout if an object is in one specific square
    */
    private List<Vector3> gridPositions = new List<Vector3>();
    
    void InitialiseList()
    {
        gridPositions.Clear();//clear positions
        for(int x=1; x < columns -1; x++)
        {
            for(int y=1; y < rows -1; y++)
            {
                //add all the possible positions where to put items
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for(int x= -1; x < columns + 1; x++)
        {
            for(int y = -1; y< rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if(x== -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

            }
        }
    }
    //function that return a random position to set an object
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }


    void LayoutObjectAtRandom(GameObject[] tileArray, int  minimum, int maximum)
    {
        //controls how many objects of a single type got instantiated
        int objectCount = Random.Range(minimum, maximum + 1);
        for(int i =0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    
    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2);//the total of enemies on each level, increases in a logarithmic way
        LayoutObjectAtRandom(enemyTiles, enemyCount,enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0.0f), Quaternion.identity);
    }
}
