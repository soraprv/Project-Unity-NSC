using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class firstTutorial : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    public GameObject BGText;

    public Rigidbody2D player;

    public TextMeshProUGUI textDisplay1;
    public GameObject tutorial;
    public GameObject continueButton1;
    void Start()
    {
        StartCoroutine(Type());
        player.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
            BGText.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (textDisplay1.text == sentences[index])
        {
            continueButton1.SetActive(true);
            tutorial.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    IEnumerator Type1()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay1.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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
            tutorial.SetActive(true);
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void NextSentence1()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay1.text = "";
            StartCoroutine(Type1());
        }
        else
        {
            textDisplay1.text = "";
            continueButton1.SetActive(false);
            tutorial.SetActive(false);
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


}
