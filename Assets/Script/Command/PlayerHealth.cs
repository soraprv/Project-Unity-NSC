using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("For Health Systems")]
    public int health;
    public int numOfHearts;
    public int PlayerNewMaxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject ItemHeal;
    public static bool Healed;
    public static bool Died = false;

    [Header("For GameOverGUI")]
    public GameObject GameOverGUI;

    [Header("For CheckPoint System")]
    private GameMaster gm;

    private void Start()
    {
        Healed = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPos;
    }

    void Update()
    {
        /*
        if(NewMaxHealth.GetNewMaxHealth == true)
        {
            numOfHearts = PlayerNewMaxHealth;
            health = PlayerNewMaxHealth;
        }
        */

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Died = true;
        }

        RegenWhenBoss2Died();
        
    }
    void TakeDamge()
    {
        health -= 1;
    }

    void Heal()
    {
        health += 1;
    }

    void RegenWhenBoss2Died()
    {
        if (Boss2Area.IsBossDied == true)
        {
            health = numOfHearts;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Boss" || col.gameObject.tag == "Enemies" || col.gameObject.tag == "Bullet")
        {
            TakeDamge();
        }

        if (col.gameObject.tag == "EnemiesJumping")
        {
            Debug.Log("testtttt");
        }

        if (col.gameObject.tag == "Heal")
        {
            Heal();
            Healed = true;
            Destroy(ItemHeal);
        }
    }
    public void GameOver()
    {
        
        Debug.Log("GAME OVER!");
        transform.position = gm.lastCheckpointPos;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Died = false;
    }
    
}
