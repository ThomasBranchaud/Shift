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
    public GameObject projectilePrefab; // The projectile prefab to instantiate
    public Transform shootPoint;       // The point from which the projectile is fired
    public float projectileSpeed = 10f;
    public float delay = 1f;
    public float timer;
    private SpriteRenderer spriteRenderer;
    public float enemyHealth = 3;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = "Search";
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        spriteRenderer = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Search"){
            patrol();
            search();
        }else if(state == "Attack"){
            patrol();
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
        // Determine the shooting direction to flip the sprite
    SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();


        // Flip the sprite horizontally based on the x-direction of the shot
        if(direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }


        // Rotate the projectile to face the target (optional)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //Debug.Log("angle is " + angle);
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
            spriteRenderer.flipX = true;  
 
        }
        if(Direction == "Left" )
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            spriteRenderer.flipX = false;

        }
}

void search(){
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
                GameObject Player = tempHit.collider.gameObject;
                state = "Attack";
                attack(Player);
                // Add logic for detecting the player here
            }
            else
            {
            }
        }
        else
        {
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
                GameObject Player = tempHit.collider.gameObject;
                state = "Attack";
                attack(Player);
                // Add logic for detecting the player here
            }
            else
            {
            }
        }
        else
        {
        }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.name == "Melee Box(Clone)" || collision.gameObject.name == "PlayerProjectile(Clone)")
    {
        Debug.Log("Enemy has been hit!");
        enemyHealth--;

        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy has died!");
            Destroy(this.gameObject, 0.125f);
        }
    }
}

}
