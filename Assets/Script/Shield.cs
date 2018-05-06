using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldPrefab;

    private GameObject shieldInstance;

    private bool shieldEnable = false;
    public bool ShieldEnable
    {
        get
        {
            return shieldEnable;
        }
        set
        {
            shieldEnable = value;

            if(shieldEnable == true)
            {
                OnGenerateShield();
            }
            else
            {
                OnDestroyShield();
            }
        }
    }

    public float chargeDelay;

    private float remainDelay;

    private void Awake()
    {
        remainDelay = 0f;

        shieldInstance = Instantiate(shieldPrefab);
        shieldInstance.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void LateUpdate()
    {
        shieldInstance.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        if (shieldEnable == false)
        {
            if(remainDelay < 0f)
            {
                ShieldEnable = true;

                remainDelay = chargeDelay;
            }
            else
            {
                remainDelay -= Time.fixedDeltaTime;
            }
        }
    }

    private void OnGenerateShield()
    {
        shieldInstance.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnDestroyShield()
    {
        shieldInstance.GetComponent<SpriteRenderer>().enabled = false;
    }
}
