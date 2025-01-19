using UnityEngine;

public class Fan : MonoBehaviour
{
    public Sprite futureSprite;
    public Sprite pastSprite;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
        anim = GetComponent<Animator>();
        anim.SetTrigger("broken");
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            Shift();
        }
    }
    
    public void Shift(){
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == futureSprite){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pastSprite;
            foreach (Transform child in transform){
                UnityEngine.Debug.Log(child.gameObject.name);
                if (child.gameObject.name == "Square"){
                    child.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureSprite;
            foreach (Transform child in transform){
                if (child.gameObject.name == "Square"){
                    child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        anim.SetTrigger("broken");
    }

}
