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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Direction = "Right";

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5)
        {
            Direction = "Right";

        }
        if(Direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5)
        {
            Direction = "Left";
            
        }
        if(Direction == "Right")
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);      
        }
        if(Direction == "Left")
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
}
}
