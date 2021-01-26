using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 3;
    public float speed = 10f;
    public float jumpPower = 15f;
    public int extraJump = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform feet;

    int jumpCount = 0;
    bool isGrounded;
    float mx;
    float jumpCoolDown;

    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    public bool wallSliding;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;

    public Animator anim;

    public bool facingRight = true;

    private void Update()
    {
        if (mx > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        if (mx < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        if (Input.GetButtonDown("Jump") && !wallSliding)
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }
            lastKeyCode = KeyCode.A;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }
            lastKeyCode = KeyCode.D;
        }
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));


        if (!isGrounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if ((facingRight && mx > 0.1f) || (!facingRight && mx < -0.1f))
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }
        CheckGround();
    }
    private void FixedUpdate()
    {
        mx = Input.GetAxis("Horizontal");
        if (!isDashing)
        {
            rb.velocity = new Vector2(mx * speed, rb.velocity.y);
            if (isGrounded)
            {
                rb.AddForce((Vector2.right * speed) * mx);
            }
            else
            {
                rb.AddForce((Vector2.right * speed / 2) * mx);
            }
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
        }


        if (wallCheck == false || isGrounded)
        {
            anim.SetBool("IsClimb", false);
            wallSliding = false;
        }
    }
    void HandleWallSliding()
    {
        rb.velocity = new Vector2(rb.velocity.x, -0.7f);

        anim.SetBool("IsClimb", true);

        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb.AddForce(new Vector2(-100, 125) * jumpPower);
            }
            else
            {
                rb.AddForce(new Vector2(100, 125) * jumpPower);
            }
        }
    }
    void jump()
    {
        if (isGrounded || jumpCount < extraJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
        }
    }
    void CheckGround()
    {
        if (Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
        }
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;

    }
}
