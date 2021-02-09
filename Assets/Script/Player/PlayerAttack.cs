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
    public float LuminousAttackRangeX;
    public float LuminousAttackRangeY;
    public float EligosAttackRangeX;
    public float EligosAttackRangeY;

    [Header("For BossAttackRange(Red)")]
    public float LuminousBossattackRangeX;
    public float LuminousBossattackRangeY;
    public float EligosBossattackRangeX;
    public float EligosBossattackRangeY;
    public static bool InBoss2Area = false;
    public static bool InBoss3Area = false;
    public static bool InBoss4Area = false;
    private bool ForCheckDebugInBoss2Area = false;
    private bool ForCheckDebugInBoss3Area = false;
    private bool ForCheckDebugInBoss4Area = false;

    [Header("For TypeOfMonsters")]
    [SerializeField] public LayerMask whatIsEnemies;
    [SerializeField] public LayerMask whatIsBoss;

    [Header("Animator")]
    public Animator animatorLunimous;
    public Animator animatorEligos;

    void Update()
    {
        ForCheckDebugInBoss2Area = InBoss2Area;
        ForCheckDebugInBoss3Area = InBoss3Area;
        ForCheckDebugInBoss4Area = InBoss4Area;

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animatorLunimous.SetTrigger("Attack");
                animatorEligos.SetTrigger("Attack");

                if (InBoss2Area)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(
                        attackPos.position, new Vector2(
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeX : EligosBossattackRangeX), 
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeY : EligosBossattackRangeY)
                        ), 
                        0, 
                        whatIsBoss
                    );
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Boss2Move>().TakeDamage(damage);
                    }
                }
                else if (InBoss3Area)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(
                        attackPos.position, new Vector2(
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeX : EligosBossattackRangeX),
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeY : EligosBossattackRangeY)
                        ),
                        0,
                        whatIsBoss
                    );
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Boss3Move>().TakeDamage(damage);
                        Debug.Log("Takedamage");
                    }
                }
                else if (InBoss4Area)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(
                        attackPos.position, new Vector2(
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeX : EligosBossattackRangeX),
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeY : EligosBossattackRangeY)
                        ),
                        0,
                        whatIsBoss
                    );
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<BossMove>().TakeDamage(damage);
                    }
                }
                else
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(
                        attackPos.position, new Vector2(
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousAttackRangeX : EligosAttackRangeX),
                            (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousAttackRangeY : EligosAttackRangeY)
                        ), 
                        0, 
                        whatIsEnemies
                    );
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<EnemyJumping>().TakeDamage(damage);
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            attackPos.position, new Vector3(
                (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousAttackRangeX : EligosAttackRangeX), 
                (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousAttackRangeY : EligosAttackRangeY),
                1
            )
        );

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            attackPos.position, new Vector3(
                (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeX : EligosBossattackRangeX),
                (GameObject.Find("Players").GetComponent<SimpleMove>().IsLuminous ? LuminousBossattackRangeY : EligosBossattackRangeY),
                1
             )
        );
    }
}