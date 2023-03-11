using Assets.player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySponder : MonoBehaviour
{
    private int enemySppedCunt = 1; 
    public float enemySpawnTime = 1;
    public GameObject enemyPrefab;

    private byte nunOfLiveingEnemy;
    private Transform enemySpawnPoint;
    private PlayerController player;
    private bool isRunning;

    [SerializeField]
    [Range(1, 20)]
    private byte maxNumOfEnemy;

    [SerializeField]
    [Range(1f, 100)]
    private float minSpeedForEnemy;

    public byte MunOfLiveingEnemy
    {
        get => nunOfLiveingEnemy;
        set => nunOfLiveingEnemy = (byte)(value > 0 ? value : 0);
    }
    public bool IsPlayerAlive { get => isRunning; set => isRunning = value; }

    private void Awake()
    {
        nunOfLiveingEnemy = 0;
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            IsPlayerAlive = true;
            player.onPlayerDying.AddListener(() => this.IsPlayerAlive = false);
            enemySpawnPoint = transform.Find("EnemySpawnPoint");
            StartCoroutine(EnemyGenerator());
        }
    }

    IEnumerator EnemyGenerator()
    {
        while (this.IsPlayerAlive)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            if (MunOfLiveingEnemy < maxNumOfEnemy)
            {
                MunOfLiveingEnemy++;

                GameObject enemyUnityGO = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
                Enemy enemy = enemyUnityGO.GetComponent<Enemy>();
                
                if (IsPlayerAlive)
                {
                    player.onPlayerDying.AddListener(enemy.PlayerKilled);
                    enemy.onObjectDestroyed.AddListener(EnemyDestroy);
                    enemy.Speed = GetSpeedForNewEnemy();
                }
                else
                {
                    enemy.PlayerKilled();
                }
            }
        }
    }

    public float GetSpeedForNewEnemy()
    {
        enemySppedCunt++;

        return (float) minSpeedForEnemy + enemySppedCunt / 10;
    }

    public void EnemyDestroy()
    {
        MunOfLiveingEnemy--;
    }
}