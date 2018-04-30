using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Boss1_4 : PatternSequencer
{
    public Boss_1 owner;

    public Bullet bulletPrefab;

    private Queue<float> hitDirection = new Queue<float>();

    protected override void Start()
    {
        owner = GetComponent<Boss_1>();

        if (owner == null)
            Destroy(this);

    }

    protected override IEnumerator PatternFlow()
    {
        isPatternRunning = true;

        owner.enableReversePattern = true;

        hitDirection.Clear();

        // 0. 잡몹 소환

        // 1. 총알 맞은 방향 기록

        // 2. 방향의 반대 방향(+5배)으로 반사

        yield return new WaitForSeconds(1f);

        float remainTime = 6f;

        while(remainTime > 0f)
        {
            remainTime -= Time.fixedDeltaTime;

            if(hitDirection.Count > 0)
            {
                float dir = hitDirection.Dequeue();
                for (int i = -2; i <= 2; ++i)
                {
                    Bullet bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = owner.transform.position;

                    bullet.direction = dir + i * 5f;

                    bullet.bulletSpeed = 5f;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        owner.enableReversePattern = false;

        yield return new WaitForSeconds(3f);

        isPatternRunning = false;

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner.enableReversePattern == false)
            return;

        Unit collisionTarget = collision.GetComponent<Unit>();

        if(collisionTarget.type == Unit.UnitType.ALLY_BULLET)
        {
            hitDirection.Enqueue(Random.Range(0f, 360f));
            hitDirection.Enqueue(Random.Range(0f, 360f));
            hitDirection.Enqueue(Random.Range(0f, 360f));
        }
    }
}
