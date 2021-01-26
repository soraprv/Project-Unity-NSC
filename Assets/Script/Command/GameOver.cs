using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverGUI;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (PlayerHealth.Died == true)
        {
            StartCoroutine(GUI());
        }
    }

    IEnumerator GUI()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
        GameOverGUI.SetActive(true);
    }
}
