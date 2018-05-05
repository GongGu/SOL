using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public Enemy enemyPrefab1;
    public Enemy enemyPrefab2;
    public Enemy enemyPrefab3;
    public Enemy enemyPrefab4;
    public Enemy enemyPrefab5;
    public Enemy enemyPrefab6;

    public Enemy boss;

    private void Awake()
    {
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(2f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab4, 1));
        spawnTimelines.Add(new SpawnTimeline(2f, enemyPrefab1, 1));

        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab2, 1));
        spawnTimelines.Add(new SpawnTimeline(1f, enemyPrefab2, 1));
        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab4, 1));
        spawnTimelines.Add(new SpawnTimeline(4f, enemyPrefab2, 1));
        spawnTimelines.Add(new SpawnTimeline(4f, enemyPrefab2, 1));
        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab5, 1));
        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab3, 1));

        spawnTimelines.Add(new SpawnTimeline(5f, enemyPrefab1, 2));
        spawnTimelines.Add(new SpawnTimeline(3f, enemyPrefab6, 1));
        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab1, 1));
        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab2, 1));
        spawnTimelines.Add(new SpawnTimeline(0f, enemyPrefab3, 1));


        // spawnTimelines.Add(new SpawnTimeline(20f, 1f, boss, 1, 0, new Vector2(0f, 0f)));


        StartCoroutine(StageFlow());
    }
}
