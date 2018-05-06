using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingUnit : Unit
{
    public int maxHP = 1;

    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            if (currentHP != value)
                OnUpdateHP();

            currentHP = value;

            if (currentHP <= 0)
            {
                OnDeath();
            }
        }
    }
    [SerializeField]
    private int currentHP = 1;

    protected virtual void OnEnable()
    {
        CurrentHP = maxHP;
    }

    protected virtual void OnDeath()
    {
        if(this is PlayerScript)
        {
            Time.timeScale = 0f;

            IngameSceneEvent.ingameScene.gameObject.SetActive(true);
        }

        Destroy(gameObject);
    }

    public virtual void OnUpdateHP()
    {

    }
}
