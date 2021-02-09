using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int health;
    public float Normalspeed;
    private float speed;
    private float dazedTime;
    public float startDazedTime;

    public float stoppingDistance;
    public float retreatDistance;

    private Transform Movement;

    void Start()
    {
        Movement = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, Movement.position) < 15)
        {
            if (Vector2.Distance(transform.position, Movement.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Movement.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, Movement.position) < stoppingDistance && Vector2.Distance(transform.position, Movement.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, Movement.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Movement.position, -speed * Time.deltaTime);
            }
        }

        if (dazedTime <= 0)
        {
            speed = Normalspeed;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
    }
}
