using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticles : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
