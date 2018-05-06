using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Boss2_3 : PatternSequencer
{
    public Part_Boss_2 leftParPrefab;
    public List<Part_Boss_2> leftParts = new List<Part_Boss_2>();

    public Boss2 owner;

    public Bullet bulletPrefab;

    private float angle;
    public float rotateSpeed;


    protected override void Start()
    {
        owner = GetComponent<Boss2>();

        if (owner == null)
            Destroy(this);

    }

    protected override IEnumerator PatternFlow()
    {

        isPatternRunning = true;

        float duration;
        float remainDuration;

        owner.pattern3On = true;

        for (int i = 0; i < 4; ++i)
        {
            leftParts.Add(Instantiate(leftParPrefab));

            leftParts[i].transform.eulerAngles = new Vector3(0f, 0f, angle + i * 90f);
            leftParts[i].transform.position = MyMath.GetRotatedPosition(
                leftParts[i].transform.eulerAngles.z, new Vector3(-0.2f, 0.9f, 0f));
        }

        duration = 1f;
        remainDuration = duration;

        while(remainDuration > 0)
        {
            remainDuration -= Time.fixedDeltaTime;

            float alpha = 1f - remainDuration / duration; //  남은 시간에 따라 0 부터 1까지 증가하는 값
            
            for(int i = 0; i < 4; ++i)
            {
                leftParts[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha); 
            }

            // remain = dura a => 0 remaindura = 0 a => 1
            yield return new WaitForFixedUpdate();
        }




        for (int i = 0; i < 4; ++i)
        {
            patternTimeStamps.Add(new PatternTimeStamp(0f, InitPatternTimeStamp(i)));
        }

        for (int i = 0; i < patternTimeStamps.Count; ++i)
        {
            Pattern currentPattern = patternTimeStamps[i].pattern;

            if (currentPattern != null) //null인 경우는 딜레이만 사용. 후딜레이 넣을 때 null 로 할 것
            {
                currentPattern.owner = GetComponent<Unit>();

                if (i > 0)
                    StartCoroutine(currentPattern.PatternFramework(patternTimeStamps[i - 1].pattern));
                else
                    StartCoroutine(currentPattern.PatternFramework(null));
            }
        }

        duration = 10f;
        remainDuration = duration;

        while (remainDuration > 0)
        {
            remainDuration -= Time.fixedDeltaTime;

            for (int i = 0; i < 4; ++i)
            {
                leftParts[i].transform.eulerAngles = new Vector3(0f, 0f, angle + i * 90f);
                leftParts[i].transform.position = MyMath.GetRotatedPosition(
                    leftParts[i].transform.eulerAngles.z, new Vector3(-0.2f, 0.9f, 0f));
            }

            angle += rotateSpeed * Time.fixedDeltaTime;
            if (angle > 360f) angle -= 360f;

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(2f);

        //foreach(var part in leftParts)
        //{
        //    Destroy(part.gameObject);
        //    leftParts.Remove(part);
        //}

        for(int i =  0; i < 4;++i)
        {
            Destroy(leftParts[0].gameObject);
            leftParts.RemoveAt(0);
        }

        patternTimeStamps.Clear();

           owner.pattern3On = false;

        yield return new WaitForSeconds(8f);

        isPatternRunning = false;

    }

    private PatternRepeat InitPatternTimeStamp(int i)
    {
        if (i >= leftParts.Count && i < 0)
            return new PatternRepeat();

        PatternRepeat repeat = new PatternRepeat();
        repeat.fireRoot = leftParts[i];
        repeat.count = 150;
        repeat.delay = 0.05f;
        repeat.bulletPrefab = bulletPrefab;
        repeat.bulletSpeed = 10f;
        repeat.deltaPos = new Vector3(0.2f, 0.9f, 0f);

        return repeat;
    }



}
