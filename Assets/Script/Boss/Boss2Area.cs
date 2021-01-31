using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Area : MonoBehaviour
{
    public GameObject Gate;
    public GameObject Boss;
    public GameObject BossHealthBar;
    public GameObject SpikeBeforePortal;
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask Player;
    private bool PlayerInArea;
    public static bool closegate = false;

    public static bool IsBossDied;
    public Transform PlayerTransform;
    public Transform PortalTransform;
    private bool TeleportFinished = false;

    void Update()
    {
        PlayerInArea = Physics2D.OverlapBox(transform.position, Size, 0, Player);
        IsBossDied = Boss2Move.BossDied;

        if (IsBossDied == true && TeleportFinished == false)
        {
            StartCoroutine(TeleportPlayer());
            TeleportFinished = true;
            Destroy(SpikeBeforePortal);
        }

        if (PlayerInArea)
        {
            PlayerAttack.InBossArea = true;
            closegate = true;
            Gate.gameObject.SetActive(true);
            Boss.gameObject.SetActive(true);
            BossHealthBar.gameObject.SetActive(true);
        }

        else if (!PlayerInArea)
        {
            PlayerAttack.InBossArea = false;
            closegate = false;
            Gate.gameObject.SetActive(false);
            Boss.gameObject.SetActive(false);
            BossHealthBar.gameObject.SetActive(false);
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(3);
        PlayerTransform.position = PortalTransform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
