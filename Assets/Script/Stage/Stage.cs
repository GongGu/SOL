using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    public struct SpawnTimeline
    {
        public float prevDelay;
        public int count;
        public Enemy spawnTarget;

        public float spawnDelay;
        public bool useRandomPos;
        public Vector2 spawnPos;
        public int spawnCondition;

        public SpawnTimeline(float _prevDelay, Enemy _spawnTarget, int _count)
        {
            prevDelay = _prevDelay;
            spawnDelay = 0.5f;

            spawnTarget = _spawnTarget;
            count = _count;

            useRandomPos = true;
            spawnCondition = 1;
            spawnPos = new Vector2();
        }

        public SpawnTimeline(float _prevDelay, float _spawnDelay, Enemy _spawnTarget, int _count, int _spawnCondition, Vector2 _spawnPos)
        {
            prevDelay = _prevDelay;
            spawnDelay = _spawnDelay;

            spawnTarget = _spawnTarget;
            count = _count;

            useRandomPos = false;
            spawnCondition = _spawnCondition;
            spawnPos = _spawnPos;
        }
    }

    protected List<SpawnTimeline> spawnTimelines = new List<SpawnTimeline>();
        
    protected IEnumerator StageFlow()
    {
        for (int i = 0; i < spawnTimelines.Count; ++i)
        {
            float remainDelay = spawnTimelines[i].prevDelay;

            while (remainDelay > 0f &&
                Enemy.enemies.Count > spawnTimelines[i].spawnCondition)
            {
                remainDelay -= Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(spawnTimelines[i].spawnDelay);

            for (int j = 0; j < spawnTimelines[i].count; ++j)
            {
                if (spawnTimelines[i].useRandomPos == true)
                    Spawn(spawnTimelines[i].spawnTarget);
                else
                    Spawn(spawnTimelines[i].spawnTarget, spawnTimelines[i].spawnPos);
            }
        }
    }

    public void Spawn(Unit spawnTarget)
    {
        Unit spawnedEnemy = Instantiate(spawnTarget); // 에너미1 프레펩으로 내용가져오고

        float randomX = Random.Range(-0.45f, 0.45f); // 범위 설정
        float randomY = Random.Range(0.25f, 0.4f);

        float worldHeight = Camera.main.orthographicSize * 2f; // 카메라의 세로 반지름 두배
        float worldWidth = worldHeight * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

        float spawnPositionX = randomX * worldWidth; // 두개 범위로 지정해서 윗부분 출현 좌표x
        float spawnPositionY = randomY * worldHeight; //y 

        spawnedEnemy.transform.position = new Vector2(spawnPositionX, spawnPositionY); //설정범위 장소에 에너미1 넣음
    }

    public void Spawn(Unit spawnTarget, Vector2 pos)
    {
        Unit spawnedEnemy = Instantiate(spawnTarget); // 에너미1 프레펩으로 내용가져오고

        spawnedEnemy.transform.position = pos;
    }
}
