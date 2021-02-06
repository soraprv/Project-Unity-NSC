using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : MonoBehaviour
{
    public int health;
    private float dazedTime;
    public float startDazedTime;

    [Header("For Petrolling")]
    [SerializeField] float speed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckpoint;
    [SerializeField] Transform wallCheckpoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For JumpAttackng")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 boxSize;
    private bool isGrounded;

    [Header("For SeeingPlayer")]
    [SerializeField] private Vector2 lineofSite;
    [SerializeField] private LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("For Point")]
    public int pointDrop;
    public GameObject point;
    public ParticleSystem pointparticles;
    public GameObject Droppoint;

    [Header("Other")]
    private Rigidbody2D enemyRB;

    [Header("For SoundEffect")]
    public GameObject sound;
    private AudioSource soundEffect;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        soundEffect = sound.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckpoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckpoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineofSite, 0, playerLayer);
        if (!canSeePlayer && isGrounded)
        {
            Petrolling();
        }
        else if (canSeePlayer && isGrounded)
        {
            JumpAttack();
        }

        if (dazedTime <= 0)
        {
            jumpHeight = 100;
        }
        else
        {
            jumpHeight = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            pointparticles.emission.SetBurst(0, new ParticleSystem.Burst(0, pointDrop));
            Instantiate(point, Droppoint.transform.position, Quaternion.identity);
            Instantiate(pointparticles, Droppoint.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void Petrolling()
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(speed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            FlipTowardsPlayer();
        }
    }

    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition < 0 && facingRight)
        {
            Flip();
        }
        else if (playerPosition > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(int damage)
    {
        soundEffect.Play();
        dazedTime = startDazedTime;
        health -= damage;
        Debug.Log("damage Taken !");
    }

    public void Knockback()
    {
        enemyRB.velocity = Vector2.up * jumpHeight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckpoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckpoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineofSite);
    }

}
