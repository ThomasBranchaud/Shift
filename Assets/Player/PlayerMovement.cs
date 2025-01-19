using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public int playerHealth;
    public int maxHearts = 5;
    public Sprite fullHeart;
    public Image[] heartImages;

    Rigidbody2D rb;

    public bool grounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
    public LayerMask defau;
    
    public string facing;

    public bool inMelee = true;

    public GameObject meleeBox;

    public string fanState = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = maxHearts;
        UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)
                     || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, defau);


        if(Input.GetKey(KeyCode.A)){
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            facing = "left";
        }else if(Input.GetKey(KeyCode.D)){
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            facing = "right";
        }else {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if(Input.GetKeyDown(KeyCode.W) && grounded){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            grounded = false;
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

        if(Input.GetMouseButtonDown(0)){
            if(inMelee){
                meleeAttack();
            } else {
                rangedAttack();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
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
        if(other.gameObject.name == "Projectile(Clone)"){
            damagePlayer(1);
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "FanUp" || other.gameObject.tag == "FanRight" || other.gameObject.tag == "FanLeft"){
            fanState = null;
        }
    }

    void meleeAttack(){
        Vector2 attackPos = (Vector2)transform.position + new Vector2(0.92f, 0f);
        if(facing == "left"){
            attackPos = (Vector2)transform.position + new Vector2(-0.92f, 0f);
        }
        GameObject atkbox = Instantiate(meleeBox, attackPos, Quaternion.identity);
        Destroy(atkbox, 0.15f);
    }

    

    void rangedAttack(){}

    void damagePlayer(int damageAmount){
        playerHealth -= damageAmount;
        Debug.Log("Current HP: " + playerHealth);
        UpdateHearts();
        if(playerHealth <= 0){
            Debug.Log("PLAYER DEATH");
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 9){
            damagePlayer(2);
        }
    }

    void UpdateHearts(){
        for(int i=0; i<maxHearts; i++){
            if(i< playerHealth){
                heartImages[i].enabled = true;
            }
            else{
                heartImages[i].enabled = false;
            }
        }
    }

}

    
