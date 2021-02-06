using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Area : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossHealthBar;
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask Player;
    private bool PlayerInArea;
    public static bool IsBossDied;

    void Update()
    {
        PlayerInArea = Physics2D.OverlapBox(transform.position, Size, 0, Player);
        IsBossDied = Boss3Move.BossDied;

        if (PlayerInArea && !IsBossDied)
        {
            PlayerAttack.InBoss3Area = true;
            Boss.gameObject.SetActive(true);
            BossHealthBar.gameObject.SetActive(true);
        }

        if (PlayerInArea && IsBossDied)
        {
            Destroy(gameObject);
        }

        else if (!PlayerInArea)
        {
            PlayerAttack.InBoss3Area = false;
            Boss.gameObject.SetActive(false);
            BossHealthBar.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
