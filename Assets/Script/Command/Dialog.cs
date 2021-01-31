using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    public GameObject BGText;

    public Rigidbody2D player;

    private bool InArea;
    public Transform Object;
    public float checkRaduis;
    public LayerMask whatIsPlayer;

    void Start()
    {
           
    }
    void FixedUpdate()
    {
        InArea = Physics2D.OverlapCircle(Object.position, checkRaduis, whatIsPlayer);
    }
    void Update()
    {
        if (InArea && index < sentences.Length - 1)
        {
            StartCoroutine(Type());
        }
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
            BGText.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
       
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            BGText.SetActive(false);
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    

}
