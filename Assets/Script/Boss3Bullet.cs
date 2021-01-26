using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    public float speed;

    public string tag;
    private Transform PlayerMovement;
    private Transform Movement;
    private Vector2 target;
    private Vector2 playerpos;
    public LayerMask HitGround;
    public Transform GroundCheck;
    public float checkObjectRadius;
    private bool isTouched;

    void Start()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").transform;
        playerpos = new Vector2(PlayerMovement.position.x, PlayerMovement.position.y);
        Movement = GameObject.FindGameObjectWithTag(tag).transform;
        target = new Vector2(Movement.position.x, Movement.position.y);
    }
    void FixedUpdate()
    {
        isTouched = Physics2D.OverlapCircle(GroundCheck.position, checkObjectRadius, HitGround);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == playerpos.x && transform.position.y == playerpos.y)
        {
            DestroyBullet();
        }
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBullet();
        }
        if (isTouched == true)
        {
            DestroyBullet();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, checkObjectRadius);
    }
}
