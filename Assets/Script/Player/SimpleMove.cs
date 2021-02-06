using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float jumpforce;
    private float moveInput;

    public static Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;


    public float LuminousRunSpeed = 60f, EligosRunSpeed = 40f;
    private float NewLuminusRunSpeed;
    public bool IsLuminous = true;

    void Start()
    {
        extraJumps = extraJumpsValue;
        
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal") ;
        rb.velocity = new Vector2(moveInput * (IsLuminous ? LuminousRunSpeed : EligosRunSpeed), rb.velocity.y);

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
        if(NewStatus.GetNewSpeed == true)
        {
            NewLuminusRunSpeed = LuminousRunSpeed + NewStatus.plusSpeed;
            LuminousRunSpeed = NewLuminusRunSpeed;
            NewStatus.GetNewSpeed = false;
        }

        if (Input.GetButtonDown("SwitchPlayer"))
        {
            gameObject.GetComponent<SwitchCharacterScript>().SwitchAvatar();
            IsLuminous = !IsLuminous;
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
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

}
