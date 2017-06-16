using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_A_Unit : MonoBehaviour {
    public SCR_Pathfinding path;
    GameObject player;
    private float moveCounter = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        path.FindPath(gameObject.transform.position, player.transform.position);
        moveCounter += Time.deltaTime;
        if (moveCounter > 1)
        {
            TakeStep();
            moveCounter = 0.0f;
        }
    }
    

    public void TakeStep()
    {
        if (path.grid.path.Count > 1)
        {
            
            transform.position = path.grid.path[0].position;
        }
    }

}
