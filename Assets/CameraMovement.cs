using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, this.gameObject.transform.position.z);

        this.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

    }
    

}
