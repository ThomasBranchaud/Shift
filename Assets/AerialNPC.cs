using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialNPC : MonoBehaviour
{
    public Vector2 WaypointL;
    public Vector2 WaypointR;
    public string Direction;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public LayerMask mask;
    public string state;
    public GameObject Player;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = "Patrol";
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Patrol"){
            patrol();
        }else if(state == "Attack"){
            attack(Player);
        }
    }
       
void attack(GameObject Player){

}
void patrol(){
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
        CastSight(rayDirection,rayOrigin);
}
    void CastSight(Vector2 rayDirection,Vector2 rayOrigin){
        for(float i=1f; i>=-1f; i-=0.1f){
            rayDirection = new Vector2(1f, i);
            RaycastHit2D tempHit = Physics2D.Raycast(rayOrigin, rayDirection, 100, mask);
            if (tempHit.collider != null) // Check if anything was hit
        {
            if (tempHit.collider.CompareTag("Player")) // Check if the first hit object is a player
            {
                Debug.Log("HIT PLAYER");
                GameObject Player = tempHit.collider.gameObject;
                state = "Attack";
                attack(Player);
                // Add logic for detecting the player here
            }
            else
            {
                Debug.Log("First object hit is not a player, it is: " + tempHit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("No objects hit");
        }
        }
    }
}
