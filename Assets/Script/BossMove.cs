using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMove : MonoBehaviour
{
    public int health;
    public float speed;
    public Slider healthBar;

    [Header("For SeeingPlayer")]
    [SerializeField] private Vector2 lineofSite;
    [SerializeField] private LayerMask playerLayer;
    private bool canSeePlayer;
    private Transform target;

    [Header("For Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    public float jumpforce;

    [Header("For RollAttack")]
    [SerializeField] Transform wallCheckpoint;
    [SerializeField] float circleRadius;
    public static float moveDirection;
    private bool facingRight = true;
    private bool checkingWall;

    [Header("For ComboAttack")]
    public static int BossJumpCount;
    private bool jumpbool;
    private bool rollbool;

    [Header("For Item drop")]
    public GameObject HealFromBoss;

    [Header("For Animation and Effects")]
    public GameObject bloodEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BossJumpCount = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (target.transform.position.x >= gameObject.transform.position.x)
        {
            moveDirection = 1;
        }
        else if (target.transform.position.x <= gameObject.transform.position.x)
        {
            moveDirection = -1;
        }
    }

    void Update()
    {
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineofSite, 0, playerLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckpoint.position, circleRadius, groundLayer);
        healthBar.value = health;

        if(BossArea.PlayerInBossArea == true)
        {
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }

        if (BossJumpCount >= 3)
        {
            
            jumpbool = false;
            rollbool = true;
        }
        else if (BossJumpCount == 0)
        {
            jumpbool = true;
            rollbool = false;
        }

        if (canSeePlayer && isGrounded)
        {
            if (jumpbool == true)
            {
                rb.velocity = Vector2.up * jumpforce;
            }
            else if (rollbool == true)
            {
                
                rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);
            }

        }

        if (checkingWall)
        {
            Flip();
        }

        if (health <= 0 || healthBar.value <= 0)
        {
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            BossJumpCount = 0;
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        BossJumpCount = 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage Taken !");
    }
    void CheckmoveDirection()
    {
        if (target.transform.position.x >= gameObject.transform.position.x)
        {
            moveDirection = 1;
        }
        else if (target.transform.position.x <= gameObject.transform.position.x)
        {
            moveDirection = -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineofSite);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckpoint.position, circleRadius);
    }
}
