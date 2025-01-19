using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite[] FutureSprites;
    public Sprite[] PastSprites;

    public Sprite futureSprite;
    public Sprite pastSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        futureSprite = FutureSprites[Random.Range(0, FutureSprites.Length)];
        pastSprite = PastSprites[Random.Range(0, PastSprites.Length)];
        this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            Shift();
        }
    }

    public void Shift(){
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == futureSprite){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pastSprite;
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
        }
    }
}