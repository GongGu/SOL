using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 복수의 Pattern 소유. 순차적으로 Pattern 을 작동시킴
public abstract class PatternSequencer : MonoBehaviour
{
    public bool isPatternRunning = false;

    [System.Serializable] // struct 를 inspector 에 표시~
    public class PatternTimeStamp
    {
        public float waitForActivate;
        public Pattern pattern;

        public PatternTimeStamp(float _waitForActivate, Pattern _pattern)
        {
            waitForActivate = _waitForActivate;
            pattern = _pattern;
        }
    }

    public List<PatternTimeStamp> patternTimeStamps = new List<PatternTimeStamp>();

    protected abstract void Awake();

    public void PatternActivate()
    {
        StartCoroutine(PatternFramework());
    }

    protected virtual IEnumerator PatternFramework()
    {
        isPatternRunning = true;

        for (int i = 0; i < patternTimeStamps.Count; ++i)
        {
            Pattern currentPattern = patternTimeStamps[i].pattern;

            yield return new WaitForSeconds(patternTimeStamps[i].waitForActivate);

            if (currentPattern != null) //null인 경우는 딜레이만 사용. 후딜레이 넣을 때 null 로 할 것
            {
                if (i > 0)
                    StartCoroutine(currentPattern.PatternFramework(patternTimeStamps[i - 1].pattern));
                else
                    StartCoroutine(currentPattern.PatternFramework(null));
            }
        }

        isPatternRunning = false;
    }


}
