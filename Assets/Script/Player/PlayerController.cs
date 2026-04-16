using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;

    [Header("Jump")]
    public float jumpVelocity = 8f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Dash")]
    public float dashForce = 12f;
    public float dashTime = 0.2f;
    public float dashCooldown = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private float x;

    // Dash
    private bool isDashing = false;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector2 dashDirection;

    // Jump
    private bool jumpRequest = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
            jumpRequest = true;

        if (Input.GetMouseButtonDown(0) && dashCooldownTimer <= 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dashDirection = (mousePos - (Vector2)transform.position).normalized;
            isDashing = true;
            dashTimer = dashTime;
            dashCooldownTimer = dashCooldown; 
        }

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            rb.linearVelocity = dashDirection * dashForce;
            if (dashTimer <= 0)
                isDashing = false;
            return;
        }

        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);

        if (x > 0) transform.eulerAngles = Vector3.zero;
        else if (x < 0) transform.eulerAngles = new Vector3(0, 180, 0);

        if (jumpRequest)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpRequest = false;
        }

        // Better jump
        if (rb.linearVelocity.y < 0)
            rb.gravityScale = fallMultiplier;
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
            rb.gravityScale = lowJumpMultiplier;
        else
            rb.gravityScale = 1f;
        // 動畫
        int State;
        if (!isGrounded)
        {
            if (rb.linearVelocity.y > 0.1f)
                State = 2; // 上升
            else
                State = 3; // 下降
        }
        else if (Mathf.Abs(x) > 0.1f)
            State = 1;     // 跑步
        else
            State = 0;     // 待機

        anim.SetInteger("State", State);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}