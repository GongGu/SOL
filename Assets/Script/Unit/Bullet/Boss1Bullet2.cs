using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Bullet2 : EnemyBullet
{

    public float originSpeed;

    private void Start()
    {
        originSpeed = bulletSpeed;
        bulletSpeed = 0f;
    }

}
