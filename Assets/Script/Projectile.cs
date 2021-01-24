using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform Movement;
    private Vector2 target;
    public LayerMask HitGround;
    public Transform GroundCheck;
    public float checkObjectRadius;
    private bool isTouched;

    void Start()
    {
        Movement = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(Movement.position.x, Movement.position.y);
    }
    void FixedUpdate()
    {
        isTouched = Physics2D.OverlapCircle(GroundCheck.position, checkObjectRadius, HitGround);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
        if (isTouched == true)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, checkObjectRadius);
    }
}
