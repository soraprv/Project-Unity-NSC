using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AutoDialog.start = 1;
        }
    }
}
