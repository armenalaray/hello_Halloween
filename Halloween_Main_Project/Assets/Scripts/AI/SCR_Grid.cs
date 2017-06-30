using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Grid : MonoBehaviour {

    public Vector2 gridSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    public SCR_Node[,] grid;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
    }
    void Update()
    {
        CreateGrid();

    }

    public void CreateGrid()
    {
        grid = new SCR_Node[gridSizeX, gridSizeY];
		Vector3 WorldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2;
        for (int x=0; x<gridSizeX;x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
				Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = (Physics2D.OverlapCircle(worldPoint, nodeRadius-.3f, unwalkableMask) == null);
                //bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));
                grid[x, y] = new SCR_Node(walkable, worldPoint, x ,y);
            }
        }
    }
    public SCR_Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - transform.position.x) / gridSize.x + 0.5f;
        float percentY = (worldPosition.y - transform.position.y) / gridSize.y + 0.5f;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    public List<SCR_Node> GetNeighbours(SCR_Node node)
    {
        List<SCR_Node> neighbours = new List<SCR_Node>();
        for(int x = -1; x<=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x==0&&y==0)||(x==-1&&y==-1) || (x == 1 && y == -1) || (x == -1 && y == 1) || (x == 1 && y == 1))
                {
                    continue;
                }
                int CheckX = node.gridX + x;
                int CheckY = node.gridY + y;
                if(CheckX >= 0 && CheckY < gridSizeX && CheckY >= 0 && CheckY<gridSizeY)
                {
                    neighbours.Add(grid[CheckX, CheckY]);
                }

            }
        }
        return neighbours;
    }
    public List<SCR_Node> path;
    /*
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y,1));
        
        if (grid!=null)
        {
            foreach (SCR_Node n in grid)
            {
                
                Gizmos.color = (n.isWalkable)?Color.white:Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                    
                
                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
    */

}
