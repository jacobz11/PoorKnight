using Assets.player;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnTime = 1;
    private Transform enemySpawnPoint;
    private byte nunOfLiveingEnemy;
    [SerializeField]
    private byte maxNumOfEnemy;
    private GameObject player;
    public byte MunOfLiveingEnemy
    {
        get => nunOfLiveingEnemy;
        set => nunOfLiveingEnemy = (byte)(value <  0 ? value : 0);
    }

    private void Awake()
    {
        nunOfLiveingEnemy = 0; 
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        enemySpawnPoint = transform.Find("EnemySpawnPoint");
        StartCoroutine(EnemyGenerator());
    }

    IEnumerator EnemyGenerator()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            if (MunOfLiveingEnemy < maxNumOfEnemy)
            {
                GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
                enemy.GetComponent<EnemyPrefab>().onObjectDestroyed.AddListener(EnemyDestroy);
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
            }
        }

    }

    public void EnemyDestroy()
    {
        this.MunOfLiveingEnemy--; 
    }
}