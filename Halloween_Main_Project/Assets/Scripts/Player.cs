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

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator animator;
    private int food;

    //private Vector2 touchOrigin = -Vector2.one;

    delegate void MyDelegate(int num1, int num2);
    MyDelegate myDelegate; 
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
    //#if UNITY_STANDALONE || UNITY_WEBPLAYER
        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        //prevent diagonal movement
        if (horizontal != 0) {
			vertical = 0;
		}

/*    #else 

        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if(myTouch == Input.touches[0];
        }*/

        if(horizontal !=0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);//we are expecting that the player collides with a wall
        }
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;

        foodText.text = "Food: " + food;

        //Debug.Log("xDir= " + xDir + " YDir= " + yDir);
        //animation triggers
        myDelegate = animationTrigger;
        myDelegate(xDir, yDir);


        base.AttemptMove<T>(xDir, yDir);

        

        RaycastHit2D hit;

        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1,moveSound2);
        }

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }

    void animationTrigger(int xDir, int yDir)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdleFront"))
        {
            if (yDir == -1)
            {
                animator.SetTrigger("pIdleFWalkFront");
                
            }
            else if(yDir == 1)
            {
                animator.SetTrigger("pIdleFWalkBack");
                Debug.Log("arriba");
            }
            else
            {
                if(xDir == 1)
                {
                    animator.SetTrigger("pIdleFWalkRight");
                }
                else
                {
                    animator.SetTrigger("pIdleFWalkLeft");
                }
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdleRight"))
        {
            if (yDir == -1)
            {
                animator.SetTrigger("pIdleRWalkFront");
            }
            else if (yDir == 1)
            {
                animator.SetTrigger("pIdleRWalkBack");
            }
            else
            {
                if (xDir == 1)
                {
                    animator.SetTrigger("pIdleRWalkRight");
                }
                else
                {
                    animator.SetTrigger("pIdleRWalkLeft");
                }
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdleLeft"))
        {
            if (yDir == -1)
            {
                animator.SetTrigger("pIdleLWalkFront");
            }
            else if (yDir == 1)
            {
                animator.SetTrigger("pIdleLWalkBack");
            }
            else
            {
                if (xDir == 1)
                {
                    animator.SetTrigger("pIdleLWalkRight");
                }
                else
                {
                    animator.SetTrigger("pIdleLWalkLeft");
                }
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdleBack"))
        {
            if (yDir == -1)
            {
                animator.SetTrigger("pIdleBWalkFront");
                Debug.Log("abajo");
            }
            else if (yDir == 1)
            {
                animator.SetTrigger("pIdleBWalkBack");
            }
            else
            {
                if (xDir == 1)
                {
                    animator.SetTrigger("pIdleBWalkRight");
                }
                else
                {
                    animator.SetTrigger("pIdleBWalkLeft");
                }
            }
        }
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
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Soda")
        {
            food += pointsPerSoda;
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
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
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
