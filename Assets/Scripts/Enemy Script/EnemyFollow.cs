using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Chase Settings")]
    [Tooltip("Enable to chase player only when within vicinity.")]
    public bool useVicinityDetection = true;

    [Tooltip("Speed at which the enemy moves.")]
    public float moveSpeed = 3f;

    [Tooltip("Layer assigned to ground objects.")]
    public LayerMask groundLayer;

    [Header("Detection Settings")]
    [Tooltip("Child object with CircleCollider2D for vicinity detection.")]
    public GameObject detectionArea;

    private Transform player;
    private Rigidbody2D rb;
    private bool isPlayerInVicinity = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found. Ensure the player has the 'Player' tag.");
        }

        // Ensure detectionArea has a trigger collider
        if (detectionArea != null)
        {
            Collider2D col = detectionArea.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = true;
            }
            else
            {
                Debug.LogError("Detection area must have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogError("Detection area not assigned.");
        }
    }

    void Update()
    {
        if (player == null) return;

        if (useVicinityDetection)
        {
            if (isPlayerInVicinity)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            MoveTowardsPlayer();
        }

        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 10000000.6f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f, groundLayer);

    }

   /* void Update()
{
    if (player == null) return;

    Vector2 direction = (player.position - transform.position).normalized;
    rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
}*/

    void MoveTowardsPlayer()
{
    Vector2 direction = (player.position - transform.position).normalized;

    // BoxCast parameters
    Vector2 boxSize = new Vector2(0.5f, 0.1f); // Width should match your enemy's feet
    Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.5f); // Adjust based on pivot
    RaycastHit2D hit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, 0.1f, groundLayer);

    Debug.DrawRay(origin, Vector2.down * 0.1f, Color.red); // Optional: visualize in Scene view

    if (hit.collider != null)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
    else
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (useVicinityDetection && other.CompareTag("Player"))
        {
            isPlayerInVicinity = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (useVicinityDetection && other.CompareTag("Player"))
        {
            isPlayerInVicinity = false;
        }
    }
}
