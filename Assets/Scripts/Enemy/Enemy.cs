using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnTime = 1;
    private Transform enemySpawnPoint;
    private void Start()
    {
        enemySpawnPoint = transform.Find("EnemySpawnPoint");
        StartCoroutine(EnemyGenerator());
    }
    IEnumerator EnemyGenerator()
    {
        yield return new WaitForSeconds(enemySpawnTime);
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
        StartCoroutine(EnemyGenerator());
    }
}