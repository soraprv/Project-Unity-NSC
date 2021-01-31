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

    public GameObject BGText;
    public Rigidbody2D player;

    public GameObject Press;
    private bool check = false;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        InArea = Physics2D.OverlapCircle(Npc.position, checkRaduis, whatIsPlayer);
    }
    void Update()
    {
        if (InArea)
        {
            if(check == false)
            {
                Press.SetActive(true);
            }
            if (check == true)
            {
                Press.SetActive(false);
            }
        }
        else
        {
            Press.SetActive(false);
        }

        if (InArea && index < sentences.Length - 1 && Input.GetKeyDown(KeyCode.F))
        {
            check = true;
            StartCoroutine(Type());
            BGText.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            Press.SetActive(false);
        }
        
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
            //BGText.SetActive(true);
            //player.constraints = RigidbodyConstraints2D.FreezeAll;

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
            Press.SetActive(true);
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


}
