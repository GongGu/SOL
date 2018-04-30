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
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(2f, enemyPrefab1, 1));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab2, 2));
        spawnTimelines.Add(new SpawnTimeline(4f, enemyPrefab2, 4));
        spawnTimelines.Add(new SpawnTimeline(4f, enemyPrefab2, 2));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab3, 1));

        spawnTimelines.Add(new SpawnTimeline(5f, enemyPrefab1, 2));

        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab1, 3));
        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab2, 2));
        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab3, 1));


        spawnTimelines.Add(new SpawnTimeline(20f, 1f, boss, 1, 0, new Vector2(0f, 0f)));


        StartCoroutine(StageFlow());
    }
}
