using UnityEngine;

public class Window : MonoBehaviour
{
    public Sprite futureSprite;
    public Sprite pastSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Shift();
        }
    }

    void Shift(){
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == futureSprite){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pastSprite;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
