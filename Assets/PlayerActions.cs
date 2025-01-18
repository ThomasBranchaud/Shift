using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private static int BLINK_COOLDOWN = 240;
    public static float BLINK_DIST = 1.5f;


    public bool grounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    int cooldown = 0;
    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // blink
        if(Input.GetKey(KeyCode.LeftShift) && cooldown == 0){
            if(Input.GetKey(KeyCode.W)){
                // blink up
                blink("up");
            }else if(Input.GetKey(KeyCode.S)){
                // blink down
                blink("down");
            }else if(Input.GetKey(KeyCode.D)){
                // blink right
                blink("right");
            }else if(Input.GetKey(KeyCode.A)){
                // blink left
                blink("left");
            }
            cooldown = BLINK_COOLDOWN;
        }
        if(cooldown > 0){
            cooldown --;
        }

    }


    void blink(string direction){
        if(direction == "up" || direction == "down"){
            float moveAmt = -BLINK_DIST;
            if(direction == "up"){
                moveAmt = BLINK_DIST;
            }

            transform.position = (Vector2)transform.position + new Vector2(0f, moveAmt);
        }else {
            float moveAmt = -BLINK_DIST;
            if(direction == "right"){
                moveAmt = BLINK_DIST;
            }

            transform.position = (Vector2)transform.position + new Vector2(moveAmt, 0f);
        }
    }
}
