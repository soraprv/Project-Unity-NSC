using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePoint : MonoBehaviour
{
    public int PointValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PointSystem.Point += PointValue;
            gameObject.SetActive(false);
        }
    }
}
