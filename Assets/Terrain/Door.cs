using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite futureDoorAir;
    public Sprite futureDoorGround;
    public Sprite pastDoor;
    public bool inAir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inAir){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureDoorAir;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = futureDoorGround;
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Shift();
        }
    }

    void Shift(){
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == pastDoor){
            if (inAir){
                this.gameObject.GetComponent<SpriteRenderer>().sprite = futureDoorAir;
            }
            else {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = futureDoorGround;
            }
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pastDoor;
        }

    }
}
