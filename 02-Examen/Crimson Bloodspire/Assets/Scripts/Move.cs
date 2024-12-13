using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveDirection = 0;

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1;
            transform.localScale = new Vector3(5, 5, 5);  // Mantiene escala normal
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1;
            transform.localScale = new Vector3(-5, 5, 5);  // Solo invierte X
        }

        animator.SetBool("Running", moveDirection != 0);
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Atacando!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}