using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    float duration;

    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        ps.Play();

        duration = ps.main.duration;

        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }


}
