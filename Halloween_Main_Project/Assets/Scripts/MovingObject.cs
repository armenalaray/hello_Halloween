using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class - allows members to be incomplete, ant to be defined into derived classes
public abstract class MovingObject : MonoBehaviour {
    public float moveTime = 0.1f;//movement of the object
    public LayerMask blockingLayer;//layer to detect colisions

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
	/* Protected Allows for inheritance of method and virtual allows 
    for checking first if the child has the method itself */
	protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1.0f / moveTime; //cheaper computationally
	}

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;//prevent from hitting own collider
        hit = Physics2D.Linecast(start, end, blockingLayer);

        //Debug.Log("hit= "+ hit.collider);
        //re-enable box collider after linecast
        boxCollider.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
            
        }
        //the move was unsuccesfull
        return false;

    }

    //moving units from one space to the other
    protected IEnumerator SmoothMovement(Vector3 end)//end specifies where to move to
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude; //returns the square magnitud of the vector = x^2 + y^2 + z^2
        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            //recalculate Remaining distance
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove <T> (int xDir, int yDir) 
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        Debug.Log("xDir=" + xDir + "YDIR" + yDir);
        //Debug.Log("canmove = "+ canMove);
        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        //si no se puede mover y hit Component tiene un component
        if(!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }

    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
