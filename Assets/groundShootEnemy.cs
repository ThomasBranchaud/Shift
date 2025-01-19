using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class groundShootEnemy : MonoBehaviour
{   public Vector2 WaypointL;
    public Vector2 WaypointR;
    public string Direction;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public LayerMask mask;
    public string state;
    public GameObject Player;
    public GameObject projectilePrefab; // The projectile prefab to instantiate
    public Transform shootPoint;       // The point from which the projectile is fired
    public float projectileSpeed = 10f;
    public float delay = 0.2f;
    public float timer;
   
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
    timer += Time.deltaTime;
    if (timer > delay)
    {
        Shoot(Player);
        timer -= delay;
    }
    Debug.Log("SHOOTING"); 
    if((transform.position.x - Player.transform.position.x) > 3)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            //Debug.Log("CHASING LEFT" + "Distance is" + (transform.position.x - Player.transform.position.x));

        }
        else if((transform.position.x - Player.transform.position.x) < 3 && (transform.position.x - Player.transform.position.x) > .1)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
    if( (Player.transform.position.x - transform.position.x) > 3)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else if((Player.transform.position.x - transform.position.x) < 3 && ((Player.transform.position.x - transform.position.x) > 0))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
}

void Shoot(GameObject Player)
    {

        // Instantiate the projectile at the shoot point
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Calculate direction to the target
        Vector2 direction = (Player.transform.position - shootPoint.position).normalized;

        // Get the Rigidbody2D of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set the projectile's velocity toward the target
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Rotate the projectile to face the target (optional)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Debug.Log("angle is " + angle);
    }
void patrol(){
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
            attack(Player);
        }
}
/*void search(){
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
        CastSight1(rayDirection,rayOrigin);
        CastSight2(rayDirection,rayOrigin);
}
    void CastSight1(Vector2 rayDirection,Vector2 rayOrigin){
        for(float i=1f; i>=-1f; i-=0.1f){
            rayDirection = new Vector2(1f, i);
            RaycastHit2D tempHit = Physics2D.Raycast(rayOrigin, rayDirection, 100, mask);
            if (tempHit.collider != null) // Check if anything was hit
        {
            if (tempHit.collider.CompareTag("Player")) // Check if the first hit object is a player
            {
                //Debug.Log("HIT PLAYER");
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
    void CastSight2(Vector2 rayDirection,Vector2 rayOrigin){
        for(float i=1f; i>=-1f; i-=0.1f){
            rayDirection = new Vector2(-1f, i);
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
    }*/
}