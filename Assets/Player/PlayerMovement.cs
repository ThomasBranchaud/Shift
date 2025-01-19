using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;

    Rigidbody2D rb;

    public bool grounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
    public LayerMask defau;
    
    public string facing;

    public bool inMelee = true;

    public GameObject meleeBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        
        // attack
        if(Input.GetMouseButtonDown(0)){
            if(inMelee){
                meleeAttack();
            } else {
                rangedAttack();
            }
        }
        
    }

    void meleeAttack(){
        Debug.Log("attacking");
        Vector2 attackPos = (Vector2)transform.position + new Vector2(0.92f, 0f);
        if(facing == "left"){
            attackPos = (Vector2)transform.position + new Vector2(-0.92f, 0f);
        }
        GameObject atkbox = Instantiate(meleeBox, attackPos, Quaternion.identity);
        Debug.Log(atkbox.name);
        Destroy(atkbox, 0.15f);

    }

    

    void rangedAttack(){}
    
}

    
