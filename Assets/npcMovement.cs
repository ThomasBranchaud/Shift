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
    private bool isKnockedBack = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth=5;
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        state = "Patrol";

        
    }

    // Update is called once per frame
    void Update()
{
    // Only process movement logic if not in knockback state
    if (!isKnockedBack)
    {
        if (state == "Patrol")
        {
            Patrol();
        }
        else if (state == "Attack")
        {
            Attack(Player);
        }
        
       
    }
}

void Attack(GameObject Player )
{

    if((transform.position.x - Player.transform.position.x) > 1)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            //Debug.Log("CHASING LEFT" + "Distance is" + (transform.position.x - Player.transform.position.x));

        }
    if( (Player.transform.position.x - transform.position.x) > 1)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            //Debug.Log("CHASING RIGHT" + "Distance is" + (Player.transform.position.x - transform.position.x));
            
            
        }
    else if(Vector2.Distance(transform.position,Player.transform.position)<1 && Vector2.Distance(transform.position,Player.transform.position)>-1)
    {
    
       // Debug.Log("ATTACKKKKKK");
    }
}

// Adjusted Patrol method
void Patrol()
{
    Vector2 rayOrigin = transform.position;
    Vector2 rayDirection = (Direction == "Right") ? Vector2.right : Vector2.left;

    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 100, mask);

    if (hit.collider != null && hit.collider.CompareTag("Player"))
    {
        Player = hit.collider.gameObject;
        state = "Attack";
    }

    if (Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
    {
        Direction = "Right";
    }
    else if (Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
    {
        Direction = "Left";
    }

    // Set velocity based on direction only if not knocked back
    if (Direction == "Right")
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
    else if (Direction == "Left")
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
    }
}

void Attack(GameObject Player)
{
    if (Vector2.Distance(transform.position, Player.transform.position) > 1)
    {
        float direction = (transform.position.x - Player.transform.position.x > 0) ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }
    else
    {
        // Enemy is close enough to attack
        rb.linearVelocity = Vector2.zero; // Stop movement to simulate attack
        Debug.Log("Enemy attacking player!");
    }
}

// Adjusted OnTriggerEnter2D for knockback
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.name == "Melee Box(Clone)")
    {
        Debug.Log("Enemy has been hit!");
        enemyHealth--;

        float enemyX = transform.position.x;
        float playerX = collision.gameObject.transform.position.x;
        Vector2 knockbackDirection = (enemyX - playerX > 0)
            ? new Vector2(3.5f, 0.75f) // Knockback to the right
            : new Vector2(-3.5f, 0.75f); // Knockback to the left

        StartCoroutine(ApplyKnockback(knockbackDirection));

        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy has died!");
            Destroy(this.gameObject, 0.125f);
        }
    }
}

IEnumerator ApplyKnockback(Vector2 knockback)
{
    isKnockedBack = true; // Temporarily disable other movement logic
    rb.linearVelocity = knockback; // Apply knockback force
    yield return new WaitForSeconds(0.2f); // Wait for knockback duration
    isKnockedBack = false; // Resume normal behavior
}
}

