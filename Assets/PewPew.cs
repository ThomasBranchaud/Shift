using UnityEngine;

public class PewPew : MonoBehaviour
{
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    void OnHit(Collider2D pew)
    {
        if (pew.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
            // Handle player damage here
        }
    }
}
