using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2FlyCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            Debug.Log("Testtttttt");
            Boss2Move.Flying = true;
        }
    }
}
