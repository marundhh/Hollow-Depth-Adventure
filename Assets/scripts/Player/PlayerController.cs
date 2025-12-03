using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpForce = 7f;

    public float WaitForattack = 0.75f;

    private bool isGrounded = true;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();   // <<---- thêm dòng này
        Move();
        Jump();
        UpdateAnimation();
    }

    // =========================
    //         ATTACK
    // =========================
    void Attack()
    {
        // Không cho tấn công khi đang trên không
        if (!isGrounded) return;

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");

            rb.velocity = new Vector2(0, rb.velocity.y);

            StartCoroutine(StopAttack());
        }
    }


    System.Collections.IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(WaitForattack); // duration animation
        isAttacking = false;
    }

    // =========================
    //       MOVEMENT
    // =========================
    void Move()
    {
        if (isAttacking) return;  // Không cho di chuyển khi đang Attack

        float horizontal = Input.GetAxisRaw("Horizontal");

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal != 0)
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, 1, 1);
    }

    // =========================
    //         JUMP
    // =========================
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // =========================
    //      UPDATE ANIM
    // =========================
    void UpdateAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        float verticalVelocity = rb.velocity.y;

        if (verticalVelocity < -0.1f)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }

        if (verticalVelocity > 0.1f)
        {
            anim.SetBool("isJumping", true);
        }
        else if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
