using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStatus : MonoBehaviour
{
    private int index;

    public GameObject QuitButton;

    private bool InArea;
    public Transform Npc;
    public float checkRaduis;
    public LayerMask whatIsPlayer;

    public GameObject BGText;
    public Rigidbody2D player;

    public GameObject Press;
    private bool check = false;
    public GameObject HP;
    public GameObject Speed;


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
            if (check == false)
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

        if (InArea && Input.GetKeyDown(KeyCode.F))
        {
            check = true;
            BGText.SetActive(true);
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            Press.SetActive(false);
            HP.SetActive(true);
            Speed.SetActive(true);
            QuitButton.SetActive(true);
        }
    }
    public void Finish()
    {
        check = false;
        BGText.SetActive(false);
        
        player.constraints = RigidbodyConstraints2D.None;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        HP.SetActive(false);
        Speed.SetActive(false);
        QuitButton.SetActive(false);

    }


}
