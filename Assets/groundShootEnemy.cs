using UnityEngine;
using System.Collections;

public class GroundShootEnemy : MonoBehaviour
{
    public Vector2 WaypointL;
    public Vector2 WaypointR;
    public float speed;
    public GameObject Player;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;
    public float attackDelay = 0.2f;
    public int enemyHealth = 3;
    public LayerMask mask;

    private Rigidbody2D rb;
    private EnemyState state = EnemyState.Patrol;
    private float attackTimer = 0f;
    private bool isKnockedBack = false;
    private string direction = "Right";

    private enum EnemyState { Patrol, Attack }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Player", "Ground");
        SetDirection(direction);
    }

    void Update()
    {
        if (isKnockedBack) return;

        switch (state)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Attack:
                Attack(Player);
                break;
        }
    }

    private void Patrol()
    {
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = direction == "Right" ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 100, mask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Player = hit.collider.gameObject;
            state = EnemyState.Attack;
            return;
        }

        if (direction == "Left" && Vector2.Distance(transform.position, WaypointL) < 0.5f)
        {
            SetDirection("Right");
        }
        else if (direction == "Right" && Vector2.Distance(transform.position, WaypointR) < 0.5f)
        {
            SetDirection("Left");
        }
    }

    private void Attack(GameObject player)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            Shoot(player);
            attackTimer = 0f;
        }

        float distance = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(distance) > 3)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(distance) * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop if within a close range
        }
    }

    private void Shoot(GameObject player)
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (player.transform.position - shootPoint.position).normalized;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.linearVelocity = direction * projectileSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void SetDirection(string newDirection)
    {
        direction = newDirection;
        rb.linearVelocity = new Vector2((direction == "Right" ? 1 : -1) * speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Melee Box(Clone)")
        {
            enemyHealth--;

            Vector2 knockbackDirection = (transform.position.x > collision.transform.position.x)
                ? new Vector2(3.5f, 0.75f)
                : new Vector2(-3.5f, 0.75f);

            StartCoroutine(ApplyKnockback(knockbackDirection));

            if (enemyHealth <= 0)
            {
                Destroy(gameObject, 0.125f);
            }
        }
    }

    private IEnumerator ApplyKnockback(Vector2 knockback)
    {
        isKnockedBack = true;
        rb.linearVelocity = knockback;
        yield return new WaitForSeconds(0.2f);
        isKnockedBack = false;
    }
}