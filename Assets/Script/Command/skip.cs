using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skip : MonoBehaviour
{
    public GameObject Video;
    public GameObject SkipButton;
    public GameObject Heart;
    public GameObject Dialog;

    public void Skip()
    {
        Video.SetActive(false);
        SkipButton.SetActive(false);
        Heart.SetActive(true);
        Dialog.SetActive(true);
    }

}
