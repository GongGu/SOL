using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT3_1 : Enemy
{
    public float spawnDelay;
    float remainDelay;
    public Enemy2 enemy2;
    float i = 0;

    private void Update()
    {
        EnemySpawn();
    }


    void EnemySpawn()
    {
        spawnDelay = 5f;
        remainDelay += Time.deltaTime;

        if(spawnDelay <= remainDelay)
        {
            StartCoroutine(TripleFire());

            Fire(i);

            remainDelay = 0;
        }

    }

    public void Fire(float i)
    {
       // Enemy2 enemy = Instantiate<Enemy2>(enemy2); // 에너미 2를 복제해서 생성
       // enemy.transform.position = this.transform.position;// enemyt3 포지션에서 출발
       // enemy.speed = 4f; // 소환된 에너미2 속도

        Enemy2 enemy = Instantiate<Enemy2>(enemy2);
        enemy.transform.position = this.transform.position;

        enemy.direction = MyMath.GetDirection(this, PlayerScript.player) + i * 5f; // 로테이션
        enemy.transform.eulerAngles = new Vector3(0f, 0f, enemy.direction - 90f);
        enemy.speed = 4f;

        enemy.movementVec2D = MyMath.DirectionToVector2(enemy.direction + i * 5f);
    }
    IEnumerator TripleFire()
    {
        
        Fire(0);
        /*

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= 1; ++i)
        {
            Fire(i - 0.5f);
        }

        yield return new WaitForSeconds(0.2f);
        */
        for (int i = -1; i <= 1; ++i)
        {
            Fire(i);
        }


        yield return new WaitForSeconds(0.2f);
    }

}
