using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static int Point;
    private int Showpoint;

    void Start()
    {
        Point = 0;
    }

    void Update()
    {
        Showpoint = Point;
    }

}
