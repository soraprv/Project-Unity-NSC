using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTeleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public static int StartTeleport = 0;
    public Transform TeleportCheck;
    public float checkRaduis;
    public LayerMask whatIsPlayer;
    private bool InArea;

    private void FixedUpdate()
    {
        InArea = Physics2D.OverlapCircle(TeleportCheck.position, checkRaduis, whatIsPlayer);
    }

    private void Update()
    {
        if(InArea && Teleportation.StartTeleport == 1 && Input.GetKeyDown(KeyCode.F))
        {
            Teleportation.StartTeleport = 0;
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TeleportCheck.position, checkRaduis);
    }
}
