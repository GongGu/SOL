using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSignal : Pattern
{

    public override IEnumerator PatternFramework(Pattern prevPattern)
    {
        PatternCircle pattern = prevPattern as PatternCircle;
        if (pattern == null)
            yield break;

        for(int i = 0; i < pattern.spawnedBulletList.Count; ++i)
        {
            if (pattern.spawnedBulletList[i] == null)
                break;

            Vector2 myPosition = pattern.spawnedBulletList[i].transform.position; // owner.transform.position; //
            Vector2 targetPosition = PlayerScript.player.transform.position;

            pattern.spawnedBulletList[i].direction = MyMath.GetDirection(myPosition, targetPosition);

            Boss1Bullet2 bullet = pattern.spawnedBulletList[i] as Boss1Bullet2;

            float speedWeight = Random.Range(0.5f, 1.5f);

            bullet.bulletSpeed = bullet.originSpeed * speedWeight;
        }
    }
}
