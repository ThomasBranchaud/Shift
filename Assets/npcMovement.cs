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
    public LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.right;

        // Set the ray direction based on the `Direction` variable
        if (Direction == "Right")
        {
            rayDirection = Vector2.right;
        }
        else if (Direction == "Left")
        {
            rayDirection = Vector2.left;
        }

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 100, mask);

        if (hit.collider != null) // Check if anything was hit
        {
            if (hit.collider.CompareTag("Player")) // Check if the first hit object is a player
            {
                Debug.Log("HIT PLAYER");
                GameObject Player = hit.collider.gameObject;
                // Add logic for detecting the player here
            }
            else
            {
                Debug.Log("First object hit is not a player, it is: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("No objects hit");
        }

     
        /*if(hit.collider != null){
            Debug.Log("hit at " + hit.distance);
            */
        
        if(Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
        {
            Direction = "Right";

        }
        if(Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
        {
            Direction = "Left";
            
        }
        if(Direction == "Right" )
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);      
        }
        if(Direction == "Left" )
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        
    
/*
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
        */
    
*/
}
}
