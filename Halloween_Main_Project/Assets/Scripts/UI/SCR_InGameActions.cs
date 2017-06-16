using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SCR_InGameActions : MonoBehaviour {

	bool paused=false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Pause ();
		}
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			if (paused == true) 
			{
				Time.timeScale = 1;
			}

			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}

	void Pause()
	{
		if (paused == false) 
		{
			Debug.Log ("Paused");
			paused = true;
			Time.timeScale = 0;
		}
		else
		{
			Debug.Log ("Unpaused");
			paused = false;
			Time.timeScale=1;
		}
	}

}
