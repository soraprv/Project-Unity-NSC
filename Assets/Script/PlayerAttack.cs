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
    public static bool InBossArea = false;
    private bool ForCheckDebugInBossArea = false;

    [Header("For TypeOfMonsters")]
    [SerializeField] public LayerMask whatIsEnemies;
    [SerializeField] public LayerMask whatIsBoss;

    void Update()
    {
        ForCheckDebugInBossArea = InBossArea;

        if (timeBtwAttack <= startTimeBtwAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (InBossArea)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(BossattackRangeX, BossattackRangeY), 0, whatIsBoss);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Boss2Move>().TakeDamage(damage);
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
    }

}
