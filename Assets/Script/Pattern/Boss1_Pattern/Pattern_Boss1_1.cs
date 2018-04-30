using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 패턴에 관련된 설명 첨부할 것.
// Boss1_1 은 세번 탄막 방사
public class Pattern_Boss1_1 : PatternSequencer
{
    public Unit owner;

    public Bullet bulletPrefab;

    protected override void Start()
    {
        owner = GetComponent<Unit>();

        if (owner == null)
            Destroy(this);

        patternTimeStamps.Add(InitPatternTimeStamp(0f, 0f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0.5f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0f));
        
        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0.5f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0.5f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0f));

        patternTimeStamps.Add(InitPatternTimeStamp(0.3f, 0.5f));

        patternTimeStamps.Add(new PatternTimeStamp(3f, null)); // 후딜레이
    }

    private PatternTimeStamp InitPatternTimeStamp(float delay, float directionRatio)
    {
        PatternCircle circle = new PatternCircle();
        circle.bulletPrefab = bulletPrefab;
        circle.position = owner.transform.position;
        circle.count = 16;
        circle.angle = 360f;
        circle.directionRatio = directionRatio;

        return new PatternTimeStamp(delay, circle);
    }
}