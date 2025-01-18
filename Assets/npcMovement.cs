using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    public Vector2 WaypointL;
    public Vector2 WaypointR;
    public string Direction;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public LayerMask playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 100, playerLayer);

     
        if(hit.collider != null){
            Debug.Log("hit at " + hit.distance);
        }
        if(Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
        {
            Direction = "Right";

        }
        if(Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
        {
            Direction = "Left";
            
        }
        if(Direction == "Right" && !(hit.collider != null))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);      
        }
        if(Direction == "Left" && !(hit.collider != null))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        else
        {Debug.Log("hit nothing");}
    

        if (hit.collider != null)
        {
            if(hit.distance>.5)
            {
                if(Direction == "Right")
                {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
                }
                else if(Direction == "Left")
                {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
                }
            }
            else
            {
                //Attack
            }
        }
        
    

}
}
