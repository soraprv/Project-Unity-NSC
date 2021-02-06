using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressF : MonoBehaviour
{
    public GameObject Press;
    private bool inArea;
    public Transform Object;
    public float CheckRadius;
    public LayerMask Player;

    void FixedUpdate()
    {
        inArea = Physics2D.OverlapCircle(Object.position, CheckRadius, Player);
    }
    void Update()
    {
        if (inArea)
        {
            Press.SetActive(true);
        }
        else
        {
            Press.SetActive(false);
        }
    }

}
