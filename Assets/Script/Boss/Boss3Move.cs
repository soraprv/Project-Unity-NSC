using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss3Move : MonoBehaviour
{
    public int health;
    public Slider healthbar;

    [Header("For SeeingPlayer (Red) ")]
    [SerializeField] private Vector2 lineofSite;
    [SerializeField] private LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("For Shooting on the air")]
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    private Transform Target;

    [Header("For ComboAttack")]
    private bool StartShooting = false;
    private int ShootingCount = 0;

    [Header("For MoveToAnotherPosition")]
    public GameObject BossPosition1;
    public GameObject BossPosition2;
    public GameObject BossPosition3;
    public GameObject BossPosition4;
    private Vector2 Pos1;
    private Vector2 Pos2;
    private Vector2 Pos3;
    private Vector2 Pos4;

    public static Rigidbody2D BossRb;

    public static bool BossDied = false;

    void Start()
    {
        BossRb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Pos1 = new Vector2(BossPosition1.transform.position.x, BossPosition1.transform.position.y);
        Pos2 = new Vector2(BossPosition2.transform.position.x, BossPosition2.transform.position.y);
        Pos3 = new Vector2(BossPosition3.transform.position.x, BossPosition3.transform.position.y);
        Pos4 = new Vector2(BossPosition4.transform.position.x, BossPosition4.transform.position.y);
    }

    void Update()
    {
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineofSite, 0, playerLayer);
        healthbar.value = health;

        if (canSeePlayer && StartShooting == true)
        {
            if (timeBtwShots <= 0)
            {
                Shoot();
                ShootingCount++;
                CheckShooting();
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        if (canSeePlayer && StartShooting == false)
        {
            StartCoroutine(StopForAMoment());
        }
        if (health <= 0 || healthbar.value <= 0)
        {
            BossDied = true;
            Destroy(healthbar.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator StopForAMoment()
    {
        yield return new WaitForSeconds(1.5f);
        Teleport();
    }

    void CheckShooting()
    {
        if (ShootingCount == 5)
        {
            StartShooting = false;
        }
        else if (ShootingCount == 10)
        {
            StartShooting = false;
        }
        else if (ShootingCount == 15)
        {
            StartShooting = false;
        }
        else if (ShootingCount == 20)
        {
            StartShooting = false;
        }
    }

    void Teleport()
    {
        if (ShootingCount == 0)
        {
            TeleporttoPos1();
        }
        else if (ShootingCount == 5)
        {
            TeleporttoPos2();
        }
        else if (ShootingCount == 10)
        {
            TeleporttoPos3();
        }
        else if (ShootingCount == 15)
        {
            TeleporttoPos4();
        }
        else if (ShootingCount == 20)
        {
            ShootingCount = 0;
        }
    }

    void TeleporttoPos1()
    {
        transform.position = Pos1;
        Boss3Move.BossRb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartShooting = true;
    }

    void TeleporttoPos2()
    {
        transform.position = Pos2;
        Boss3Move.BossRb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartShooting = true;
    }

    void TeleporttoPos3()
    {
        transform.position = Pos3;
        Boss3Move.BossRb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartShooting = true;
    }

    void TeleporttoPos4()
    {
        transform.position = Pos4;
        Boss3Move.BossRb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartShooting = true;
    }

    void Shoot()
    {
        Instantiate(bullet1, transform.position, Quaternion.identity);
        Instantiate(bullet2, transform.position, Quaternion.identity);
        Instantiate(bullet3, transform.position, Quaternion.identity);
        Instantiate(bullet4, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineofSite);
    }
}
