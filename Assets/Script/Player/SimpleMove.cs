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
    public GameObject DoubleJump_BW;
    public GameObject DoubleJumpGameObject;
    private Image DobleJumpCD;

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
    private float SwitchCoolDown;
    public float startTimeCoolDown;
    public Image SwitchPlayerCD;
    private bool SwitchPlayerisCoolDown = false;

    // variable for dash

    [Header("Dash")]
    private float dashSpeed;
    public float NormaldashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float startDashCoolDown;
    private float DashCoolDown;
    public GameObject DashCD_BW;
    public GameObject DashCDGameObject;
    private Image DashCD;
    private bool DashisCoolDown = false;

    [Header("gride")]
    public float startgrideTime;
    public float startgrideCoolDown;
    private float grideTime;
    private int gridedirection;
    private float grideCoolDown;
    public GameObject Glide_BW;
    public GameObject GlideGameObject;
    private Image GlideCD;

    [Header("stomp")]
    public float startstompTime;
    public float startstompCoolDown;
    private float stompTime;
    private int stompdirection;
    private float stompCoolDown;
    public GameObject Stomp_BW;
    public GameObject StompGameObject;
    private Image StompCD;
    private bool StompisCoolDown = false;

    void Start()
    {
        extraJumps = IsLuminous ? 0 : 1;

        rb = GetComponent<Rigidbody2D>();

        SwitchPlayerCD.fillAmount = 0;

        DobleJumpCD = DoubleJump_BW.GetComponent<Image>();
        DobleJumpCD.fillAmount = 0;

        DashCD = DashCD_BW.GetComponent<Image>();
        DashCD.fillAmount = 0;
        dashTime = startDashTime;

        GlideCD = Glide_BW.GetComponent<Image>();
        GlideCD.fillAmount = 0;
        grideTime = startgrideTime;

        stompTime = startstompTime;
        StompCD = Stomp_BW.GetComponent<Image>();
        StompCD.fillAmount = 0;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * (IsLuminous ? LuminousRunSpeed : EligosRunSpeed), rb.velocity.y);
        if (IsLuminous)
        {
            animatorLuminous.SetFloat("Speed", Mathf.Abs(moveInput * LuminousRunSpeed));
            ShowSkillLuminious();
        }
        else
        {
            animatorEligos.SetFloat("Speed", Mathf.Abs(moveInput * EligosRunSpeed));
            ShowSkillEligos();
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
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
        if (SwitchCoolDown <= 0)
        {
            if (Input.GetButtonDown("SwitchPlayer") && SwitchPlayerisCoolDown == false)
            {
                gameObject.GetComponent<SwitchCharacterScript>().SwitchAvatar();
                IsLuminous = !IsLuminous;
                SwitchPlayerisCoolDown = true;
                SwitchPlayerCD.fillAmount = 1;
                SwitchCoolDown = startTimeCoolDown;
            }
        }
        else
        {
            if (SwitchPlayerisCoolDown)
            {
                SwitchPlayerCD.fillAmount -= 1 / startTimeCoolDown * Time.deltaTime;
                if (SwitchPlayerCD.fillAmount <= 0)
                {
                    SwitchPlayerCD.fillAmount = 0;
                    SwitchPlayerisCoolDown = false;
                }
            }
            SwitchCoolDown -= Time.deltaTime;
        }

        //Jump and DoubleJump
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
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpforce;
        }

        //Luminous Dash/ Stomp/ Climb
        if (IsLuminous)
        {
            if (direction == 0)
            {
                if (DashCoolDown <= 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A) && DashisCoolDown == false)
                    {
                        animatorLuminous.SetTrigger("Dash");
                        direction = 1;
                        DashisCoolDown = true;
                        DashCD.fillAmount = 1;
                        DashCoolDown = startDashCoolDown;
                    }
                    else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) && DashisCoolDown == false)
                    {
                        animatorLuminous.SetTrigger("Dash");
                        direction = 2;
                        DashisCoolDown = true;
                        DashCD.fillAmount = 1;
                        DashCoolDown = startDashCoolDown;
                    }
                }
                else
                {
                    if (DashisCoolDown)
                    {
                        DashCD.fillAmount -= 1 / DashCoolDown * Time.deltaTime;
                        if (DashCD.fillAmount <= 0)
                        {
                            DashCD.fillAmount = 0;
                            DashisCoolDown = false;
                        }
                    }
                    DashCoolDown -= Time.deltaTime;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                    dashSpeed = 0f;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    dashSpeed = NormaldashSpeed;
                    if (direction == 1)
                    {
                        transform.Translate(Time.deltaTime * dashSpeed * -1f, 0, 0);
                    }
                    else if (direction == 2)
                    {
                        transform.Translate(Time.deltaTime * dashSpeed * 1f, 0, 0);
                    }
                }
            }


            if (stompdirection == 0)
            {
                if (stompCoolDown <= 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S) && StompisCoolDown == false)
                    {
                        stompdirection = 1;
                        StompisCoolDown = true;
                        StompCD.fillAmount = 1;
                        stompCoolDown = startstompCoolDown;
                        animatorLuminous.SetBool("IsStomp",true);
                    }
                }
                else
                {
                    if (StompisCoolDown)
                    {
                        StompCD.fillAmount -= 1 / stompCoolDown * Time.deltaTime;
                        if (StompCD.fillAmount <= 0)
                        {
                            StompCD.fillAmount = 0;
                            StompisCoolDown = false;
                        }
                    }
                    stompCoolDown -= Time.deltaTime;
                }
            }
            else
            {
                if (stompTime <= 0)
                {
                    stompdirection = 0;
                    stompTime = startstompTime;
                    rb.gravityScale = 3;
                    animatorLuminous.SetBool("IsStomp",false);
                }
                else
                {
                    stompTime -= Time.deltaTime;
                    if (stompdirection == 1)
                    {
                        rb.gravityScale = 10;
                    }
                }
            }
        }
        else //Eligos DoubleJump^ gride/
        {
            if (gridedirection == 0)
            {
                if (grideCoolDown <= 0)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        gridedirection = 1;
                        grideCoolDown = startgrideCoolDown;
                        animatorEligos.SetBool("IsGride", true);
                    }
                }
                else
                {
                    grideCoolDown -= Time.deltaTime;
                }
            }
            else
            {
                if (grideTime <= 0 || isGrounded)
                {
                    gridedirection = 0;
                    grideTime = startgrideTime;
                    rb.gravityScale = 3;
                    animatorEligos.SetBool("IsGride", false);
                }
                else
                {
                    grideTime -= Time.deltaTime;
                    if (gridedirection == 1)
                    {
                        rb.gravityScale = 1;
                    }
                }
            }
        }
    }

    void ShowSkillLuminious()
    {
        DashCD_BW.SetActive(true);
        DashCDGameObject.SetActive(true);
        Stomp_BW.SetActive(true);
        StompGameObject.SetActive(true);
        Glide_BW.SetActive(false);
        GlideGameObject.SetActive(false);
        DoubleJump_BW.SetActive(false);
        DoubleJumpGameObject.SetActive(false);
    }

    void ShowSkillEligos()
    {
        DashCD_BW.SetActive(false);
        DashCDGameObject.SetActive(false);
        Stomp_BW.SetActive(false);
        StompGameObject.SetActive(false);
        Glide_BW.SetActive(true);
        GlideGameObject.SetActive(true);
        DoubleJump_BW.SetActive(true);
        DoubleJumpGameObject.SetActive(true);
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
}