using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    // Variables de movimiento
    public float moveSpeed = 2f;
    public float patrolDistance = 3f;
    public float waitTime = 2f;
    private float waitCounter;
    private Vector2 startPosition;
    private bool movingRight = true;

    // Variables de detección del jugador
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public Transform player;
    private bool playerDetected = false;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    // Variables de estado
    private bool isWaiting = false;
    public bool isDead = false;
    
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (rb == null || animator == null)
        {
            Debug.LogError("Faltan componentes necesarios en " + gameObject.name);
            enabled = false;
            return;
        }

        startPosition = transform.position;
        waitCounter = waitTime;
    }

    void Update()
    {
        if (isDead || rb == null || animator == null) return;

        float distanceToPlayer = 0f;
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            playerDetected = distanceToPlayer < detectionRange;

            if (playerDetected)
            {
                ChasePlayer(distanceToPlayer);
            }
            else
            {
                Patrol();
            }

            animator.SetBool("IsWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            if (waitCounter <= 0)
            {
                isWaiting = false;
                waitCounter = waitTime;
                movingRight = !movingRight;
            }
            return;
        }

        float distanceFromStart = transform.position.x - startPosition.x;

        if ((movingRight && distanceFromStart > patrolDistance) ||
            (!movingRight && distanceFromStart < -patrolDistance))
        {
            isWaiting = true;
            return;
        }

        float direction = movingRight ? 1 : -1;
        transform.localScale = new Vector3(direction * 4, 4, 4);
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void ChasePlayer(float distanceToPlayer)
    {
        if (player == null) return;

        float direction = transform.position.x < player.position.x ? 1 : -1;
        transform.localScale = new Vector3(direction * 4, 4, 4);

        if (distanceToPlayer > attackRange)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;
        
        isDead = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.SetTrigger("Death");
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}