using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    public static bool PlayerInBossArea = false;
    private bool PlayerInThisArea;
    [SerializeField] private Vector2 InputSize;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        PlayerInThisArea = Physics2D.OverlapBox(transform.position, InputSize, 0, playerLayer);
        if (PlayerInThisArea)
        {
            PlayerInBossArea = true;
        }
        else if (!PlayerInThisArea)
        {
            PlayerInBossArea = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, InputSize);
    }
}