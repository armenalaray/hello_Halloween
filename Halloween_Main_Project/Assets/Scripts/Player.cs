using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;

    public float restartLevelDelay = 1.0f;
    public Text foodText;


    private Animator animator;
    private int food;

	// Use this for override an inherited function
	protected override void Start () {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;

        foodText.text = "Food: " + food;

        //call base function on MovingObject
        base.Start();
	}
	
    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }


	// Update is called once per frame
	void Update () {
        //si no es el turno del jugador no ejecuta lo siguiente
        if (!GameManager.instance.playersTurn) return;
        //Debug.Log(GameManager.instance.playersTurn);
        int horizontal =0 ;
        int vertical = 0;

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        //prevent diagonal movement
        if (horizontal != 0) {
			vertical = 0;
		}
        if(horizontal !=0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);//we are expecting that the player collides with a wall
        }
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;

        foodText.text = "Food: " + food;

        base.AttemptMove<T>(xDir, yDir);

        //RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }
    //check triggers with food and soda
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Exit")
        {
            //to invoke that method after an specific time
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if(other.tag == "Food")
        {
            food += pointsPerFood;
            foodText.text = "+" + pointsPerFood + " Food: " + food;
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Soda")
        {
            food += pointsPerSoda;
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
            other.gameObject.SetActive(false);
        }
    }
    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;//casting for protection
        hitWall.DamageWall(wallDamage);

        //set animation playerChop
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        foodText.text = "-" + loss + " Food: " + food;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if(food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
