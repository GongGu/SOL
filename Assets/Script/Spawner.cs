using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab; // 프레펩 오브젝트 
    public float spawnDeley; //스폰 시간
    private float remainDeley; //스폰시간까지 남은 시간(딜레이)
    void Awake()
    {

    }
    void Start()
    {

    }

    void Update()
    {
        spawnDeley = 1f; 
        remainDeley += Time.deltaTime;

        if (spawnDeley<remainDeley) // 
        {
            Spawn();
           

            remainDeley = 0; // 스폰했으니 다시 0으로 초기화
        }
        
    }
    void Spawn()
    {
        GameObject enemy1 = Instantiate(enemyPrefab); // 에너미1 프레펩으로 내용가져오고

        float randomX = Random.Range(-0.45f, 0.45f); // 범위 설정
        float randomY = Random.Range(0.25f, 0.4f);

        float worldHeight = Camera.main.orthographicSize * 2f; // 카메라의 세로 반지름 두배
        float worldWidth = worldHeight * Camera.main.aspect; // 카메라 세로 비율로 가로 비율 추출

        float spawnPositionX = randomX * worldWidth; // 두개 범위로 지정해서 윗부분 출현 좌표x
        float spawnPositionY = randomY * worldHeight; //y 

        enemy1.transform.position = new Vector2(spawnPositionX,spawnPositionY); //설정범위 장소에 에너미1 넣음

    }
    
}
