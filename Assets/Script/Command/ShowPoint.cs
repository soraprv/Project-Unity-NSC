using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPoint : MonoBehaviour
{
    public TextMeshProUGUI ShowScore;
    private int Score;


    void Start()
    {

    }

    void Update()
    {
        Score = PointSystem.Point;
        ShowScore.text = "" + Score;
    }
}
