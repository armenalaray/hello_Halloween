using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Projectiles : MonoBehaviour {

    float counter;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.position += Vector3.right;
        counter += Time.deltaTime;
        if (counter >= 0.5)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionenter(Collision hit)
    {
        if(hit.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }

}
