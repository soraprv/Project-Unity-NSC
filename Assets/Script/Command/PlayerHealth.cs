using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject ItemHeal;
    public static bool Healed;
    public static bool TouchEnemyJumping;

    public GameObject GameOverGUI;

    public static bool Died = false; 

    private void Start()
    {
        Healed = false;
        TouchEnemyJumping = false;
    }

    void Update()
    {
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
            StartCoroutine(GUI());
            //gameObject.SetActive(false);
            Died = true;
        }
        
    }
    void TakeDamge()
    {
        health -= 1;
    }

    void Heal()
    {
        health += 1;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator GUI()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
        GameOverGUI.SetActive(true);
    }
    
}
