using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingUnit
{
    public static List<Enemy> enemies = new List<Enemy>();

    protected virtual void Awake()
    {
        base.OnEnable();

        enemies.Add(this);
    }

    protected virtual void OnDestroy()
    {
        enemies.Remove(this);
    }

}
