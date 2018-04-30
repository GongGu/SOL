using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDirection : EnemyBullet
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        this.transform.eulerAngles = new Vector3(0f, 0f, direction - 90f);
    }
}
