using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB4 : Enemy
{

    private float spawnDelay;
    private float remainDelay;
    private float i = 0;

    public EnemyB1 enemyPrefab;

    private void Start()
    {
        Spawn(i);
    }

    private void FixedUpdate()
    {
        EnemySpawn();
    }

    public void EnemySpawn()
    {
        spawnDelay = 10f;
        remainDelay += Time.deltaTime;

        if (spawnDelay <= remainDelay)
        {
            StartCoroutine(EnemyFire());

            Spawn(i);

            remainDelay = 0;
        }

    }

    public void Spawn(float i)
    {
        EnemyB1 spawnEnemy = Instantiate<EnemyB1>(enemyPrefab);
        spawnEnemy.transform.position = this.transform.position;

        float direction = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position) + i * 30f;

        spawnEnemy.transform.eulerAngles = new Vector3(0f, 0f, direction - 90f);
        spawnEnemy.moveSpeed = 8f;
    }

    private IEnumerator EnemyFire()
    {
        for(int i = -2; i < 2; ++i)
        {
            Spawn(i);
        }

        yield return new WaitForSeconds(0.5f);

    }
}
