using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Sprite futureElevator;
    public Sprite pastElevator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = futureElevator;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Shift();
        }
    }

    void Shift(){
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == futureElevator){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pastElevator;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureElevator;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
