using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Transform enemySpawnPoint;
    public float spawnDelay;
    public float spawnTime;
    public bool stopSpawning = false;

    private void Start()
    {
        enemySpawnPoint = transform.Find("EnemySpawnPoint");
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }
    public void SpawnObject()
    {
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
        if (stopSpawning)
            CancelInvoke("SpawnObject");
    }
}