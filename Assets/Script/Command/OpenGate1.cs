using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate1 : MonoBehaviour
{
    public GameObject Gate;
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask Box;
    private bool BoxTouched;

    void Update()
    {
        BoxTouched = Physics2D.OverlapBox(transform.position, Size, 0, Box);

        if (BoxTouched && Boss2Area.closegate == false)
        {
            Gate.gameObject.SetActive(false);
        }
        else if (BoxTouched && Boss2Area.closegate == true)
        {
            Gate.gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
