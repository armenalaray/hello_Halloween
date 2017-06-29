using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_A_Turn_Unit : MonoBehaviour {
    //Falta Terminar
    SCR_Pathfinding path;
    GameObject player;
    Animator anim;
    private float moveCounter = 0.0f;
    public int enemyHP = 2;
    GameManager gameM;
    int turnDelay;
    bool myTurn;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        path = GameObject.FindGameObjectWithTag("A*").GetComponent<SCR_Pathfinding>();
        gameM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        path.FindPath(gameObject.transform.position, player.transform.position);

        TakeStep();
        CheckHP();
        if(gameM.enemiesMoving==true)
        {
            turnDelay +=1;
        }
        if(turnDelay%2==0&&gameM.playersTurn==true)
        {
            myTurn = true;
        }

    }


    public void TakeStep()
    {
        if (path.grid.path.Count > 1&&gameM.enemiesMoving==true&&myTurn==true)
        {
            Vector3 prevpos;
            prevpos = transform.position;
            //transform.position = path.grid.path[0].position;
            if(path.grid.path[0].position == player.transform.position)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if(transform.position==player.transform.position)
            {

                transform.position = path.grid.path[0].position;

            }
            myTurn = false;
        }


    }

    void CheckHP()
    {
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            enemyHP -= 1;
        }
        
    }
}
