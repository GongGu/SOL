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
        if (type == UnitType.ALLY_LIVING_UNIT)
            Application.Quit();
        Destroy(gameObject);
    }
}
