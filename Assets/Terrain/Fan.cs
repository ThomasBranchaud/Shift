using UnityEngine;

public class Fan : MonoBehaviour
{
    public Sprite futureSprite;
    public Sprite pastSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
    }

}
