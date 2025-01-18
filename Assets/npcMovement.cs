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
    public string state;
    private GameObject Player;
    public float enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth=5
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        state = "Patrol";

        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Patrol")
        {
            Patrol();
        }
        if(state == "Attack")
        {
            Attack(Player);
        }
        
       

}
void Attack(GameObject Player )
{

    if((transform.position.x - Player.transform.position.x) > 1)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            Debug.Log("CHASING LEFT" + "Distance is" + (transform.position.x - Player.transform.position.x));

        }
    if( (Player.transform.position.x - transform.position.x) > 1)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            Debug.Log("CHASING RIGHT" + "Distance is" + (Player.transform.position.x - transform.position.x));
            
            
        }
    else if(Vector2.Distance(transform.position,Player.transform.position)<1 && Vector2.Distance(transform.position,Player.transform.position)>-1)
    {
    
        Debug.Log("ATTACKKKKKK");
    }
    
    
}
void Patrol()
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
                Player = hit.collider.gameObject;
                state = "Attack";
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
        
        if(state == "Attack")
        {
            Attack(Player);
        }
    }
}

