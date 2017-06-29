using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Credits : MonoBehaviour {
    float animTime=10;
    float timetoChange;
    SCR_MenuScript camScript;
	// Use this for initialization
	void Start () {
        timetoChange = animTime;
        camScript = FindObjectOfType<Camera>().GetComponent<SCR_MenuScript>();
	}
	
	// Update is called once per frame
	void Update () {
        timetoChange -= Time.deltaTime;
        if (timetoChange >= 0)
        {
            transform.position += (Vector3.up);
        }
        else if(timetoChange<0)
        {
            timetoChange = animTime;
            gameObject.transform.parent.gameObject.SetActive(false);
            camScript.mainMenu.SetActive(true);
        }
	}
}
