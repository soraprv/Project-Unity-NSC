using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            BossMove.BossJumpCount++;
        }
    }
}
