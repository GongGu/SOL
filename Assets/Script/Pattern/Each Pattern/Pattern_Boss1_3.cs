using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 네개 모서리에 발사하는 패턴
public class Pattern_Boss1_3 : PatternSequencer
{
    public Boss_1 owner;

    public Bullet bulletPrefab;

    protected override void Start()
    {
        owner = GetComponent<Boss_1>();

        if (owner == null)
            Destroy(this);

        // 4개에서 뿅뿅뿅 쏘는 패턴을 4개 만들기
        for (int i = 0; i < 4; ++i)
        {
            patternTimeStamps.Add(new PatternTimeStamp(0f, InitPatternTimeStamp(i)));
        }

        patternTimeStamps.Add(new PatternTimeStamp(3f, null));
    }

    protected override IEnumerator PatternFlow()
    {
        isPatternRunning = true;

        owner.enableSetupPartTransform = false;

        // 4개를 모서리로 이동시키는 패턴
        {
            float partDistance = 0.5f;

            List<Vector2> toPos = new List<Vector2>();
            List<Vector2> fromPos = new List<Vector2>();
            List<float> posCursors = new List<float>();

            toPos.Add(new Vector2(+MyMath.GetWorldHalfSize().x - partDistance, +MyMath.GetWorldHalfSize().y - partDistance));
            toPos.Add(new Vector2(+MyMath.GetWorldHalfSize().x - partDistance, -MyMath.GetWorldHalfSize().y + partDistance));
            toPos.Add(new Vector2(-MyMath.GetWorldHalfSize().x + partDistance, -MyMath.GetWorldHalfSize().y + partDistance));
            toPos.Add(new Vector2(-MyMath.GetWorldHalfSize().x + partDistance, +MyMath.GetWorldHalfSize().y - partDistance));

            for(int i = 0; i < owner.parts.Count; ++i)
            {
                fromPos.Add(owner.parts[i].transform.position);
                posCursors.Add(0f);
            }

            float duration = 1.5f;
            float remainTime = duration;

            while (remainTime > 0f)
            {
                for (int i = 0; i < owner.parts.Count; ++i)
                {
                    posCursors[i] += Time.fixedDeltaTime / duration;

                    owner.parts[i].transform.position =
                        Vector2.Lerp(fromPos[i], toPos[i], posCursors[i]);

                    owner.parts[i].transform.eulerAngles =
                        new Vector3(0f, 0f, MyMath.GetDirection(owner.parts[i], PlayerScript.player) + 90f);
                }

                remainTime -= Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < patternTimeStamps.Count; ++i)
        {
            Pattern currentPattern = patternTimeStamps[i].pattern;

            yield return new WaitForSeconds(patternTimeStamps[i].waitForActivate);

            if (currentPattern != null) //null인 경우는 딜레이만 사용. 후딜레이 넣을 때 null 로 할 것
            {
                currentPattern.owner = GetComponent<Unit>();

                if (i > 0)
                    StartCoroutine(currentPattern.PatternFramework(patternTimeStamps[i - 1].pattern));
                else
                    StartCoroutine(currentPattern.PatternFramework(null));
            }
        }

        owner.enableSetupPartTransform = true;

        isPatternRunning = false;
    }

    private PatternRepeat InitPatternTimeStamp(int i)
    {
        if (i >= owner.parts.Count && i < 0)
            return new PatternRepeat();

        PatternRepeat repeat = new PatternRepeat();
        repeat.fireRoot = owner.parts[i];
        repeat.count = 10;
        repeat.delay = 0.2f;
        repeat.target = PlayerScript.player;
        repeat.bulletPrefab = bulletPrefab;
        repeat.bulletSpeed = 10f;
        repeat.deltaDirection = 90f;

        return repeat;
    }
}
