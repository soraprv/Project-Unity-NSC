using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStatus : MonoBehaviour
{
    public static bool GetNewMaxHealth;
    public static bool GetNewSpeed;
    public static int plusHealth = 1;
    public static float plusSpeed = 1;

    private void Start()
    {
        GetNewMaxHealth = false;
        GetNewSpeed = false;
    }

    public void UpHealthStatus()
    {
        if(PointSystem.Point > 0)
        {
            GetNewMaxHealth = true;
            PointSystem.Point -= 1;
        }
    }

    public void UpSpeedStatus()
    {
        if (PointSystem.Point > 0)
        {
            GetNewSpeed = true;
            PointSystem.Point -= 1;
        }
    }
}
