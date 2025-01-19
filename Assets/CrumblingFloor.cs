using UnityEngine;

public class CrumblingFloor : MonoBehaviour
{
    public bool future = true;
    private Animator anim;
    public bool fallen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }



    public void Shift(){
        if (fallen == false){
            anim.SetTrigger("swap");
        }
        else if (future){
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            future = false;
        }
        else {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            future = true;
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Shift();
        }
    }

    public void Fallen(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        anim.SetTrigger("fallen");
        fallen = true;
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            anim.SetTrigger("fall");
            anim.Play("CrumblingFloor", 0, 0.0f);
            fallen = true;
        }
    }
}
