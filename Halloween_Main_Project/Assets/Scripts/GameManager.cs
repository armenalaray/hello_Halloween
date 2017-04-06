﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;//singleton -static means that the variable belongs to the class rather than being an instanciation of the object. 
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playersTurn = true;

    private int level = 3;
	// Use this for initialization
	void Awake () {

        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}
	
    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        enabled = false;
    }
    public void Prueba(int a)
    {
        //Howdy
        a++;
    }
      
}
