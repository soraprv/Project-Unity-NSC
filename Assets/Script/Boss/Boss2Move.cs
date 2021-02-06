using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Move : MonoBehaviour
{
    public int health;
    public Slider healthbar;

    [Header("For SeeingPlayer (Red) ")]
    [SerializeField] private Vector2 lineofSite;
    [SerializeField] private LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("For Flip")]
    public Transform PlayerDirectionCheck;
    private bool PlayerDirection;
    [SerializeField] private Vector2 size;
    private bool facingToPlayer = true;

    [Header("For Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;

    [Header("For Shooting on the air")]
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public GameObject FlyCheck;
    private Transform Target;
    public static bool Flying = false;

    [Header("For Move To Close Player (Yellow) ")]
    private bool ClosePlayer;
    [SerializeField] private Vector2 CloseArea;
    public static float moveDirection;
    public float speed;

    [Header("For ComboAttack")]
    private bool StartShooting = false;
    public static bool BossDied = false;

    [Header("For SoundEffect")]
    public GameObject sound;
    private AudioSource soundEffect;

    public static Rigidbody2D BossRb;

    void Start()
    {
        BossRb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        soundEffect = sound.GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineofSite, 0, playerLayer);
        ClosePlayer = Physics2D.OverlapBox(transform.position, CloseArea, 0, playerLayer);
        PlayerDirection = Physics2D.OverlapBox(PlayerDirectionCheck.position, size, 0, playerLayer);
        healthbar.value = health;

        if (PlayerDirection)
        {
            facingToPlayer = true;
        }
        else if (PlayerDirection == false)
        {
            facingToPlayer = false;
        }

        if(facingToPlayer == false)
        {
            Flip();
            facingToPlayer = true;
        }

        if (isGrounded && !canSeePlayer)
        {
            StartShooting = false;
        }
        else if (isGrounded && canSeePlayer && !ClosePlayer)
        {
            StartShooting = false;
        }
        else if (isGrounded && canSeePlayer & ClosePlayer)
        {
            StartShooting = true;
        }

        if (!isGrounded && canSeePlayer && !ClosePlayer)
        {
            StartShooting = false;
        }

        if(transform.position.y >= FlyCheck.transform.position.y)
        {
            Flying = true;
        }

        if (StartShooting == true)
        {
            Fly();
            if (Flying)
            {
                Boss2Move.BossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                if (timeBtwShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
        if (StartShooting == false)
        {
            Flying = false;
            StayOnGround();
        }

        if(health <= 0 || healthbar.value <= 0)
        {
            BossDied = true;
        }
    }

    void Fly()
    {
        BossRb.gravityScale = -5;
    }


    void StayOnGround()
    {
        CheckDirection();
        BossRb.gravityScale = 3;
        Boss2Move.BossRb.constraints = RigidbodyConstraints2D.None;
        Boss2Move.BossRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        BossRb.velocity = new Vector2(speed * moveDirection, BossRb.velocity.y);
    }

    void CheckDirection()
    {
        if (Target.transform.position.x >= gameObject.transform.position.x)
        {
            moveDirection = 1;
            facingToPlayer = false;
        }
        else if (Target.transform.position.x <= gameObject.transform.position.x)
        {
            moveDirection = -1;
            facingToPlayer = true;
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(int damage)
    {
        soundEffect.Play();
        health -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineofSite);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, CloseArea);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(PlayerDirectionCheck.position, size);
    }
}
