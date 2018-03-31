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

            Vector2 myPosition = pattern.spawnedBulletList[i].transform.position;
            Vector2 targetPosition = PlayerScript.player.transform.position;

            pattern.spawnedBulletList[i].direction = MyMath.GetDirection(myPosition, targetPosition);
        }
    }
}
