using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float vel;
	//public GameObject _camera;
	//public Transform objetivo;
	//public float suavidad;
	//public Quaternion objRot;
	//Vector3 compensar;
	// Use this for initialization
	void Start () {
		//compensar = _camera.transform.position - this.transform.position;//distancia entre oobjetivo y camara
		//objRot = _camera.transform.rotation;
        
	}
	
	// Update is called once per frame
	void Update () {
		//movimiento del jugador
		transform.position += new Vector3 (Input.GetAxis("Horizontal") * vel*Time.deltaTime,Input.GetAxis("Vertical")*vel*Time.deltaTime, 0); 

		if (Input.GetKey (KeyCode.Space)) {
					
		}
	}

	void FixedUpdate(){
       
		//Vector3 objetivoCam = this.transform.position + compensar;
		//_camera.transform.LookAt(transform);
		//objRot -= this.transform.rotation.SetFromToRotation (_camera.transform.position, objetivoCam); 
		//_camera.transform.position = Vector3.Lerp (_camera.transform.position, objetivoCam, suavidad * Time.deltaTime);
        //_camera.transform.position = objetivoCam;
        //Debug.Log(objetivoCam);
    }
}
