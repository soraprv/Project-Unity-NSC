using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMaxHealth : MonoBehaviour
{
    public static bool GetNewMaxHealth;

    private void Start()
    {
        GetNewMaxHealth = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetNewMaxHealth = true;
        }
    }
}
