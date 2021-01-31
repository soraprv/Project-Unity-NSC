using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour
{
    [Header("For SeeingPlayer (Red) ")]
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask playerLayer;
    private bool PlayerInArea;

    [Header("For Sound")]
    public GameObject SceneSound;
    private AudioSource BgSound;

    void Start()
    {
        BgSound = SceneSound.GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerInArea = Physics2D.OverlapBox(transform.position, Size, 0, playerLayer);

        if (PlayerInArea)
        {
            SceneSound.SetActive(true);
        }
        else
        {
            SceneSound.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
