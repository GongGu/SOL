using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : Bullet
{
    public Effects effect;

    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        Unit targetUnit = collider.GetComponent<Unit>();
        if (targetUnit == null)
            return;

        if (type == UnitType.ALLY_BULLET &&
            targetUnit.type == UnitType.ENEMY_LIVING_UNIT)
        { //아군 총알이 적유닛에 충돌

            if (effect == null)
                return;

            effect.transform.position = transform.position;
            effect.transform.eulerAngles = transform.eulerAngles;

            Instantiate(effect);
        }

    }

}

