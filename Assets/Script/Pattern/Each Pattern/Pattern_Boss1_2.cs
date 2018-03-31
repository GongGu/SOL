using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Boss1_2 는 순차적으로 방사한 후, 플레이어 방향으로 이동시키는 패턴.
public class Pattern_Boss1_2 : PatternSequencer
{
    public Unit owner;

    public Bullet bulletPrefab;

    protected override void Start()
    {
        owner = GetComponent<Unit>();

        if (owner == null)
            Destroy(this);

        InitCircleSignal();
        InitCircleSignal();
        InitCircleSignal();
        InitCircleSignal();
        InitCircleSignal();

        patternTimeStamps.Add(new PatternTimeStamp(3f, null));
    }

    void InitCircleSignal()
    {
        PatternCircle circle = new PatternCircle();
        circle.bulletPrefab = bulletPrefab;
        circle.position = owner.transform.position;
        circle.count = 16;
        circle.angle = 360f;
        circle.distance = 2f;
        circle.delay = 0.03f;
        circle.bulletSpeed = 8f;

        patternTimeStamps.Add(new PatternTimeStamp(0f, circle));

        PatternSignal signal = new PatternSignal();
        signal.owner = owner;

        // circle 패턴의 탄막이 모두 생성된 후 0.1 초 뒤에 발사
        patternTimeStamps.Add(new PatternTimeStamp(circle.count * circle.delay + 0.5f, signal));
    }
}

