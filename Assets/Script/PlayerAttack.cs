using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("For Attack")]
    public int damage;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;

    [Header("For EnemiesAttackRange(Yellow)")]
    public float attackRangeX;
    public float attackRangeY;

    [Header("For BossAttackRange(Red)")]
    public float BossattackRangeX;
    public float BossattackRangeY;

    [Header("For ScanTypeOfEnemies(Blue)")]
    [SerializeField] private Vector2 scansize;
    [SerializeField] public LayerMask whatIsEnemies;
    [SerializeField] public LayerMask whatIsBoss;
    private bool BossIsScaned;

    private void Start()
    {

    }

    void Update()
    {
        BossIsScaned = Physics2D.OverlapBox(transform.position, scansize, 0, whatIsBoss);
        if (timeBtwAttack <= startTimeBtwAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (BossIsScaned)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(BossattackRangeX, BossattackRangeY), 0, whatIsBoss);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<BossMove>().TakeDamage(damage);
                    }
                }
                else
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<EnemyJumping>().TakeDamage(damage);
                    }
                }

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack = Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(BossattackRangeX, BossattackRangeY, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, scansize);
    }

}
