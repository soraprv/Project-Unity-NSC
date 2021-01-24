using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressDialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    private bool InArea;
    public Transform Npc;
    public float checkRaduis;
    public LayerMask whatIsPlayer;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        InArea = Physics2D.OverlapCircle(Npc.position, checkRaduis, whatIsPlayer);
    }
    void Update()
    {
        if (InArea && index < sentences.Length - 1 && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Type());
        }

        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }


}
