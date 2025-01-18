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
        RaycastHit hit;
        if(Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
        {
            Direction = "Right";

        }
        if(Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
        {
            Direction = "Left";
            
        }
        if(Direction == "Right" && !(Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f)))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);      
        }
        if(Direction == "Left" && !(Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f)))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }


    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries



        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))

        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); 
            Debug.Log("Did Hit"); 
        }
        else
        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); 
            Debug.Log("Did not Hit"); 
        }

    

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
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
