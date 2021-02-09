using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStatus : MonoBehaviour
{
    public static bool GetNewMaxHealth;
    public static bool GetNewLuminiusSpeed;
    public static bool GetNewEligosSpeed;
    public static int plusHealth = 1;
    public static float plusSpeed = 1;

    private void Start()
    {
        GetNewMaxHealth = false;
        GetNewLuminiusSpeed = false;
        GetNewEligosSpeed = false;
    }

    public void UpHealthStatus()
    {
        if(PointSystem.Point > 0)
        {
            GetNewMaxHealth = true;
            PointSystem.Point -= 1;
        }
    }

    public void UpSpeedLuminus()
    {
        if (PointSystem.Point > 0)
        {
            GetNewLuminiusSpeed = true;
            PointSystem.Point -= 1;
        }
    }

    public void UpSpeedEligos()
    {
        if (PointSystem.Point > 0)
        {
            GetNewEligosSpeed = true;
            PointSystem.Point -= 1;
        }
    }
}
