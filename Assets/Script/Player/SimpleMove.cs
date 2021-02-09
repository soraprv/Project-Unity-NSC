using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMove : MonoBehaviour
{
    [Header("Animator")]
    public Animator animatorLuminous;
    public Animator animatorEligos;

    [Header("Jump")]
    public float jumpforce;
    private float moveInput;
    private int extraJumps;

    public static Rigidbody2D rb;

    private bool facingRight = true;

    [Header("Radius")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    


    [Header("RunSpeed")]
    public float LuminousRunSpeed = 60f, EligosRunSpeed = 40f;
    public bool IsLuminous = true;
    private float NewLuminusRunSpeed;
    private float NewEligosRunSpeed;

    // variable for SwitchPlayerCooldown
    [Header("SwitchPlayerCooldown")]
    private float CoolDown;
    public float startTimeCoolDown;
    public Image SwitchPlayerCD;
    private bool isCoolDown = false;

    //dash
    [Header("Dash")]
    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    void Start()
    {
        extraJumps = IsLuminous? 0 : 1;
        
        rb = GetComponent<Rigidbody2D>();

        SwitchPlayerCD.fillAmount = 0;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal") ;
        rb.velocity = new Vector2(moveInput * (IsLuminous ? LuminousRunSpeed : EligosRunSpeed), rb.velocity.y);
        animatorLuminous.SetFloat("Speed", Mathf.Abs(moveInput * LuminousRunSpeed));
        animatorEligos.SetFloat("Speed", Mathf.Abs(moveInput * EligosRunSpeed));

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        //dash
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput *  (IsLuminous ? LuminousRunSpeed : EligosRunSpeed), rb.velocity.y);
            if (isGrounded)
            {
                rb.AddForce((Vector2.right * (IsLuminous ? LuminousRunSpeed : EligosRunSpeed)) * moveInput);
            }
            else
            {
                rb.AddForce((Vector2.right *  (IsLuminous ? LuminousRunSpeed : EligosRunSpeed) / 2) * moveInput);
            }
        }
    }

    void Update()
    {
        // Up Status(speed) For Luminus
        if (NewStatus.GetNewLuminiusSpeed == true)
        {
            NewLuminusRunSpeed = LuminousRunSpeed + NewStatus.plusSpeed;
            LuminousRunSpeed = NewLuminusRunSpeed;
            NewStatus.GetNewLuminiusSpeed = false;
        }
        else if (NewStatus.GetNewEligosSpeed == true)
        {
            NewEligosRunSpeed = EligosRunSpeed + NewStatus.plusSpeed;
            EligosRunSpeed = NewEligosRunSpeed;
            NewStatus.GetNewEligosSpeed = false;
        }

        // SwitchPlayer with CoolDown
        if (CoolDown <= 0)
        {
            if (Input.GetButtonDown("SwitchPlayer") && isCoolDown == false)
            {
                gameObject.GetComponent<SwitchCharacterScript>().SwitchAvatar();
                IsLuminous = !IsLuminous;
                isCoolDown = true;
                SwitchPlayerCD.fillAmount = 1;
                CoolDown = startTimeCoolDown;
            }
        }
        else
        {
            if (isCoolDown)
            {
                SwitchPlayerCD.fillAmount -= 1 / startTimeCoolDown * Time.deltaTime;
                if (SwitchPlayerCD.fillAmount <= 0)
                {
                    SwitchPlayerCD.fillAmount = 0;
                    isCoolDown = false;
                }
            }
            CoolDown -= Time.deltaTime;
        }

        //Jump
        if (isGrounded == true)
        {
            extraJumps = IsLuminous ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !isGrounded)
        {
            animatorEligos.SetTrigger("DoubleJump");
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpforce;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
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
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }
            lastKeyCode = KeyCode.D;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    IEnumerator Dash (float direction)
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
