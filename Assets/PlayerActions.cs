using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float blinkDist = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* BLINK TEMPORARILY DISABLED

        // blink
        if(Input.GetKey(KeyCode.LeftShift)){
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
        }

        */
    }


    void blink(string direction){
        if(direction == "up" || direction == "down"){
            float moveAmt = -blinkDist;
            if(direction == "up"){
                moveAmt = blinkDist;
            }

            transform.position = (Vector2)transform.position + new Vector2(0f, moveAmt);
        }else {
            float moveAmt = -blinkDist;
            if(direction == "right"){
                moveAmt = blinkDist;
            }

            transform.position = (Vector2)transform.position + new Vector2(moveAmt, 0f);
        }
    }
}
