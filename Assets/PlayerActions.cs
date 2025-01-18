using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private static int BLINK_COOLDOWN = 1;
    public static float BLINK_DIST = 2.5f;


    public bool grounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
    public LayerMask platformLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;
    }

    public int cooldown = 0;
    bool blinkedInAir = false;
    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) 
                    || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayer);
        if(grounded){
            blinkedInAir = false;
        }
        // blink
        if(Input.GetKey(KeyCode.Space) && cooldown == 0){
            if(!blinkedInAir){
                if(Input.GetKey(KeyCode.A)){
                    // blink left
                    blinkLeft();
                }else if(Input.GetKey(KeyCode.D)){
                    // blink right
                    blinkRight();
                }else if(Input.GetKey(KeyCode.W)){
                    // blink up
                    blinkUp();
                }else if(Input.GetKey(KeyCode.S)){
                    // blink down
                    if(!grounded){
                        blinkDown();
                    }
                }
                cooldown = BLINK_COOLDOWN * 60;
            }

            if(!grounded){
                blinkedInAir = true;
            }else {
                blinkedInAir = false;
            }
        }
        
    
        if(cooldown > 0){
        cooldown --;
        }

    }

    void blinkUp(){
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, BLINK_DIST, groundLayer);

        float moveAmt = BLINK_DIST;
        if(hit.collider != null){
            moveAmt = hit.distance;
        }

        transform.position = (Vector2)transform.position + new Vector2(0f, moveAmt);
    }

    void blinkDown(){
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, BLINK_DIST, groundLayer);

        float moveAmt = -BLINK_DIST;
        if(hit.collider != null){
            moveAmt = -hit.distance;
        }

        transform.position = (Vector2)transform.position + new Vector2(0f, moveAmt);
    }

    void blinkRight(){
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, BLINK_DIST, groundLayer);

        float moveAmt = BLINK_DIST;
        if(hit.collider != null){
            moveAmt = hit.distance;
        }

        transform.position = (Vector2)transform.position + new Vector2(moveAmt, 0f);
    }

    void blinkLeft(){
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, BLINK_DIST, groundLayer);

        float moveAmt = -BLINK_DIST;
        if(hit.collider != null){
            moveAmt = -hit.distance;
        }

        transform.position = (Vector2)transform.position + new Vector2(moveAmt, 0f);
    }
}
