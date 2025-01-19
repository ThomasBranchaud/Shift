using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smoothSpeed = 1.5f;
    public float threshold = 2f;
    public GameObject player;
    
    private Vector3 lastPlayerPosition;
    public bool useSmoothMovement = false;

    void Start()
    {
        player = gameObject.GetComponent<GameManager>().player;
    }

    void LateUpdate()
    {
        
        
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, this.gameObject.transform.position.z);
        this.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
        

    }
    

}
