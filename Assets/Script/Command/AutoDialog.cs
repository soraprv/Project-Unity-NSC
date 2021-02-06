using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoDialog : MonoBehaviour
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

    private bool check = false;

    public static int start = 0;
    public GameObject key;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        InArea = Physics2D.OverlapCircle(Object.position, checkRaduis, whatIsPlayer);
    }
    void Update()
    {
        if (InArea)
        {
            check = true;
        }
        else
        {
            check = false;
        }
        if (index < sentences.Length - 1 && check == true && AutoDialog.start == 1)
        {
            StartCoroutine(Type());
            BGText.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            
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
            continueButton.SetActive(false);
            BGText.SetActive(false);
            start = 0;
            Destroy(key);
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
