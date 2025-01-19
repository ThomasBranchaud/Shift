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
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth=5;
        mask = LayerMask.GetMask("Player","Ground");
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        state = "Patrol";
        spriteRenderer = GetComponent<SpriteRenderer>();

        
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
            spriteRenderer.flipX = false;
            //Debug.Log("CHASING LEFT" + "Distance is" + (transform.position.x - Player.transform.position.x));

        }
    if( (Player.transform.position.x - transform.position.x) > 1)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            spriteRenderer.flipX = true;
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
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    else if (Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
    {
        Direction = "Left";
        spriteRenderer.flipX = !spriteRenderer.flipX;
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


// Adjusted OnTriggerEnter2D for knockback
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.name == "Melee Box(Clone)" || collision.gameObject.name == "PlayerProjectile(Clone)")
    {
        //Debug.Log("Enemy has been hit!");
        enemyHealth--;

        float enemyX = transform.position.x;
        float playerX = collision.gameObject.transform.position.x;
        Vector2 knockbackDirection = (enemyX - playerX > 0)
            ? new Vector2(3.5f, 0.75f) // Knockback to the right
            : new Vector2(-3.5f, 0.75f); // Knockback to the left

        StartCoroutine(ApplyKnockback(knockbackDirection));

        if (enemyHealth <= 0)
        {
            //Debug.Log("Enemy has died!");
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

