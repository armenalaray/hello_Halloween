using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2.0f;
    public float turnDelay = .1f;
    public static GameManager instance = null;//singleton -static means that the variable belongs to the class rather than being an instanciation of the object. 
    public int playerFoodPoints;
    [HideInInspector]
    public bool playersTurn = true;
	public Animator playerAnimator;

    private BoardManagerFixed boardScript;
    private Text levelText;
    private GameObject levelImage;
    public int level;
    private List<Enemy> enemies;
    [HideInInspector]
    public bool enemiesMoving;//check if they're moving
    [HideInInspector]
    public bool doingSetup;//prevent player from moving while doing setup
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
        enemies = new List<Enemy>();

        boardScript = GetComponent<BoardManagerFixed>();
        //InitGame();
        playerFoodPoints = 100;
        level = 1;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        
        InitGame();
        level++;

    }

    void OnEnable()
    {
        //tell our onlevelfinishedloading function to start listening for a scene change event as soon as this script is enabled
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //tell our onlevelfinishedloading function to stop listening for a sene change event as soon as this script is diabled.
        //remember to have an unsubscription for every delegate sou subscribe to
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        //clear once the level inicializes
        enemies.Clear();
        boardScript.SetupScene(level);

		playerAnimator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator>();

    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " Levels, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup)
            return;

		//playerAnimator.SetFloat("horizontal", 0);
		//playerAnimator.SetFloat("vertical", 0);
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        //wait for instanciation of enemies
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for(int i =0;i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }

}
