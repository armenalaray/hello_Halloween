using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_A_Unit : MonoBehaviour {
    SCR_Pathfinding path;
    GameObject player;
    Animator anim;
    private float moveCounter = 0.0f;
    public int enemyHP=2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        path = GameObject.FindGameObjectWithTag("A*").GetComponent<SCR_Pathfinding>();
        
    }

    // Update is called once per frame
    void Update () {
        path.FindPath(gameObject.transform.position, player.transform.position);

        moveCounter += Time.deltaTime;
        if (moveCounter > 1.5f)
        {
            TakeStep();
            moveCounter = 0.0f;
        }


        CheckHP();
    }
    

    public void TakeStep()
    {
        if (path.grid.path.Count > 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.position = path.grid.path[0].position;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }


    }
    void CheckHP()
    {
        if(enemyHP<= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            enemyHP -= 1;
        }

    }

}
