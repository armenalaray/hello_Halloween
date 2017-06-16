using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Enemy_2 : MonoBehaviour {

    public GameObject projectile;
    public GameObject gridParent;
    SCR_Grid grid;
    Vector3 targetPos = Vector3.zero;
    SCR_Node targetNode;
    bool gotPos;

    void Start()
    {
        grid = gridParent.GetComponent<SCR_Grid>();
        Invoke("MoveToRandomArea", 0);

        InvokeRepeating("Shoot", 2, 2);
    }

    void MoveToRandomArea()
    {
        gotPos = false;

        while (gotPos == false)
        {
            targetPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));
            targetNode = grid.NodeFromWorldPoint(targetPos);
            if (targetNode.isWalkable)
            {
                transform.position = targetPos;
                gotPos = true;
                Debug.Log(gotPos);
            }

        }
        Debug.Log("imdone");
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);

    }

}
