using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 복수의 Pattern Sequancer 를 소유하고 관리. 랜덤하게 시퀀서 작동시킴
public class PatternActivator : MonoBehaviour
{
    public List<PatternSequencer> patterns = new List<PatternSequencer>();
    public Queue<PatternSequencer> patternQueue = new Queue<PatternSequencer>();
    private PatternSequencer lastActivatedPattern; // 패턴이 연속으로 발동을 안하게 마지막 패턴의 정보를 가지고있을거임

    private void FixedUpdate()
    {
        PatternProcess();
    }

    private void PatternProcess()
    {
        if (patternQueue.Count == 0) // 패턴을 다 실행했으면 새로 채워넣음
        {
            PatternSequencer selectPattern = patterns[Random.Range(0, patterns.Count - 1)]; // 유니티 사이즈 지정값

            // 새로 채워넣을 때, lastActivatedPattern 을 가장 먼저 채워넣지 않도록 설정
            while (selectPattern != null &&
                selectPattern == lastActivatedPattern) // 마지막에 발동한 패턴이 연속으로 발동되지 않도록 처리
            {
                selectPattern = patterns[Random.Range(0, patterns.Count - 1)]; // select random pattern
            }

            if (selectPattern == null)
                return;

            patternQueue.Enqueue(selectPattern);
            patterns.Remove(selectPattern);

            // 나머지 패턴들을 Queue 에 마저 채워넣음
            while (patterns.Count > 0) 
            {
                selectPattern = patterns[Random.Range(0, patterns.Count - 1)]; // select random pattern

                patternQueue.Enqueue(selectPattern);
                patterns.Remove(selectPattern);
            }
        }
        else // 실행할 패턴이 남아있다
        {
            if (patternQueue.Peek().isPatternRunning == false) // 패턴이 돌아가고 있지 않다!
            {
                if(patternQueue.Peek() != lastActivatedPattern) // 패턴이 아직 실행되지 않았다!
                {
                    lastActivatedPattern = patternQueue.Peek();

                    lastActivatedPattern.PatternActivate(); // is Pattern Runngin 을 true 로.
                }
                else // 패턴 실행이 끝났다!
                {
                    lastActivatedPattern = patternQueue.Dequeue();

                    patterns.Add(lastActivatedPattern);
                }
            }
        }
    }
}
