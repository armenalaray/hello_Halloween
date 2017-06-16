using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Node{

    public bool isWalkable;
    public Vector3 position;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public SCR_Node parent;

    public SCR_Node(bool _isWalkable, Vector3 _position, int _gridX, int _gridY)
    {
        isWalkable = _isWalkable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;


    }
    public int fCost
    {
        get{
            return gCost + hCost;
        }
    }
}
