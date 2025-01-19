using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private bool immune = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if(counter >= 3){
            immune = false;
        }else if(counter >= 120){
            immune = false;
        }
        counter++;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(!immune){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(!immune){
            Destroy(this.gameObject);
        }
    }
}
