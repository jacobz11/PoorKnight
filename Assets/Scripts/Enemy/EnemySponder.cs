using Assets.player;
using System;
using System.Collections;
using UnityEngine;

public class EnemySponder : MonoBehaviour
{
    public float enemySpawnTime = 1;
    public GameObject enemyPrefab;

    private byte nunOfLiveingEnemy;
    private Transform enemySpawnPoint;
    private GameObject player;

    [SerializeField]
    private byte maxNumOfEnemy;

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
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
                enemy.GetComponent<Enemy>().onObjectDestroyed.AddListener(EnemyDestroy);
                this.MunOfLiveingEnemy++;
            }
        }
    }

    public void EnemyDestroy()
    {
        this.MunOfLiveingEnemy--; 
    }
}