using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public Enemy enemyPrefab1;
    public Enemy enemyPrefab2;
    public Enemy enemyPrefab3;

    public Enemy boss;

    private void Awake()
    {
        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab1, 3));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab2, 3));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab3, 1));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab1, 3));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab1, 3));

        spawnTimelines.Add(new SpawnTimeline(30f, 1f, boss, 1, 0, new Vector2(0f, 3f)));


        StartCoroutine(StageFlow());
    }
}
