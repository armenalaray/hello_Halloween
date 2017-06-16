using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Pathfinding : MonoBehaviour {
    public SCR_Grid grid;
   
    void Awake()
    {
        grid = GetComponent<SCR_Grid>();
    }
    
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        SCR_Node startNode = grid.NodeFromWorldPoint(startPos);
        SCR_Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<SCR_Node> openSet = new List<SCR_Node>();
        HashSet<SCR_Node> closedSet = new HashSet<SCR_Node>();
        openSet.Add(startNode);
        while(openSet.Count>0)
        {
            SCR_Node currentNode = openSet[0];
            for(int i = 1; i<openSet.Count; i++)
            {
                if(openSet[i].fCost<currentNode.fCost || openSet[i].fCost == currentNode.fCost&& openSet[i].hCost<currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode ==targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (SCR_Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.isWalkable||closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost||!openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }

                }
            }
        }
    }
    void RetracePath(SCR_Node startNode, SCR_Node endNode)
    {
        List<SCR_Node> path = new List<SCR_Node>();
        SCR_Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
    }
    public void TakeStep()
    {
        if (grid.path.Count >= 0)
        {
            transform.position = grid.path[0].position;
        }
    }
    int GetDistance(SCR_Node nodeA, SCR_Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX-nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if(distX>distY)
        {
            return 40*distY+10*(distX-distY);
        }
        else
        {
            return 40 * distX + 10 * (distY - distX);

        }

    }
}
