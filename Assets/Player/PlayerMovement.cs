using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;

    static int PLAYER_LAYER = 3;
    static int PLATFORM_LAYER = 7;

    Rigidbody2D rb;

    public bool grounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    
    private int platformLayerIndex;
    private bool droppingThroughPlatform;

    public string fanState = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformLayerIndex = LayerMask.NameToLayer("Platform");
    }
    public Vector2 v1 = new Vector2();

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.A)){
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }else if(Input.GetKey(KeyCode.D)){
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }else {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) 
                    || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayer);
        if(Input.GetKeyDown(KeyCode.W) && grounded){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            grounded = false;
        }
        
        if (Input.GetKeyDown(KeyCode.S) && Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayer))
        {
            // StartCoroutine(DropThroughPlatform());
            transform.position = (Vector2)transform.position + new Vector2(0f, -0.6f);
        }
        if (fanState != null){
            UnityEngine.Debug.Log("Not Stopping");
            switch (fanState){
                case "Right":
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x + 0.5f, rb.linearVelocity.y);
                    break;
                
                case "Left":
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x - 0.5f, rb.linearVelocity.y);
                    break;

                case "Up":
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + 0.2f);
                    break;
            }
        }

        
    }

    private IEnumerator DropThroughPlatform()
    {
        // Temporarily disable collision with the platform
        Physics2D.IgnoreLayerCollision(PLAYER_LAYER, PLATFORM_LAYER, true);
        Debug.Log("Start falling");


        // Wait for a short time to ensure the player falls through
        yield return new WaitForSeconds(0.3f);

        // Re-enable collision with the platform
        Physics2D.IgnoreLayerCollision(PLAYER_LAYER, PLATFORM_LAYER, false);
        Debug.Log("Stop falling");

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (droppingThroughPlatform && collision.gameObject.layer == PLATFORM_LAYER)
        {
            Physics2D.IgnoreLayerCollision(PLAYER_LAYER, PLATFORM_LAYER, true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        UnityEngine.Debug.Log("Here");
        if (other.gameObject.CompareTag("Elevator")){
            UnityEngine.Debug.Log("Level End");
        }
        switch(other.gameObject.tag){
            case "FanRight":
                fanState = "Right";
                break;
            
            case "FanUp":
                fanState = "Up";
                break;
            
            case "FanLeft":
                fanState = "Left";
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "FanUp" || other.gameObject.tag == "FanRight" || other.gameObject.tag == "FanLeft"){
            fanState = null;
        }
    }
    
}

    
