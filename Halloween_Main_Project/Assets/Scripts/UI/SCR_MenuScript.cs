using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SCR_MenuScript : MonoBehaviour {

    public GameObject quitMenu;
    public GameObject optionMenu;
    public GameObject mainMenu;
	public GameObject creditsUI;

    // Use this for initialization
    void Start ()
    {
        mainMenu.SetActive(true);
        quitMenu.SetActive(false);
        optionMenu.SetActive(false);
		creditsUI.SetActive (false);
    }
    public void ExitPress()
    {
        quitMenu.SetActive(true);
        mainMenu.SetActive(false);

    }
    public void OptionPress()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);

    }
    public void BackPress()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
	public void CreditsPress()
	{
		creditsUI.SetActive (true);
		mainMenu.SetActive(false);

	}
	public void CreditsBackPress()
	{
		
	}
    public void CancelQuitPress()
    {
        quitMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void StartLevel1()
    {
        //Escribir Nombre del nivel
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
