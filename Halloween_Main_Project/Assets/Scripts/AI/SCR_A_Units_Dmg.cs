using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_A_Units_Dmg : MonoBehaviour {


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            gameObject.tag = "Untagged";

        }   
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                col.GetComponent<Player>().food;
            }

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.tag = "Dmg";

        }
    }
    */
}
