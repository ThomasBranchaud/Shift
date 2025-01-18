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
        if(Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
        {
            Direction = "Right";

        }
        if(Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
        {
            Direction = "Left";
            
        }
        if(Direction == "Right")
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);      
        }
        if(Direction == "Left")
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }

       

     /*Vector2 point = currentPoint.position - transform.position;
     if(currentPoint == WaypointR.transform)
     { 
        rb.linearVelocity = new Vector2(speed, 0);
     }
     else
     {
        rb.linearVelocity = new Vector2(-speed, 0);
     }
     if(Vector2.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == WaypointR.transform)
     {
        currentPoint = WaypointL.transform;
     }   
    if(Vector2.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == WaypointL.transform)
     {
        currentPoint = WaypointR.transform;
     }   
    }*/
}
}
