using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
         x = Input.GetAxis("Horizontal");
         // cooldown
         if (dashCooldownTimer > 0)
             dashCooldownTimer -= Time.deltaTime;

         x = Input.GetAxis("Horizontal");

         //Dash
         if (isDashing)
           {
            dashTimer -= Time.deltaTime;

            rb.linearVelocity = dashDirection * dashForce;

             anim.SetInteger("State", 4);

              if (dashTimer <= 0)
               isDashing = false;

              return;
            }

         // run
         rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
         if (x > 0)
             transform.eulerAngles = new Vector3(0, 0, 0);
         else if (x < 0)
             transform.eulerAngles = new Vector3(0, 180, 0);

         //Dash
         if (Input.GetMouseButtonDown(0) && dashCooldownTimer <= 0)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dashDirection = (mousePos - (Vector2)transform.position).normalized;

                isDashing = true;
                dashTimer = dashTime;
                dashCooldownTimer = dashCooldown;
            }

            // Idel
            int state = 0;
            float yVelocity = rb.linearVelocity.y;

            if (yVelocity > 0.1f)
                state = 2;
            else if (yVelocity < -0.1f)
                state = 3;
            else if (Mathf.Abs(x) > 0.1f)
                state = 1;
            else
                state = 0;

            anim.SetInteger("State", state);
        }
    }
