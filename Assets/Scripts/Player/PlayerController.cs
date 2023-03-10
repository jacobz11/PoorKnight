using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.player
{
    internal class PlayerController : MonoBehaviour
    {
        public bool isCollidingWithEnemy;
        public bool isOnGround = true;
        public GameObject arrowPrefab;

        private int score;
        private float lastShootTime;
        private Rigidbody2D rigidbodyPlayer;
        private Transform arrowSpawnPoint;
        private SpriteRenderer flipPlayer;
        private Animator anim;
        private IEnumerator ShotArrowCoroutine;

        public UnityEvent onPlayerDying = new UnityEvent();
        [SerializeField]
        [Range(5f, 1000f)]
        private float jump;
        [SerializeField]
        private float speed = 15f;
        [SerializeField] 
        private float shootCooldown = 0.45f;
        [SerializeField]
        private UI playerUi;
        private bool isFirstShot;

        public bool IsJump => Input.GetKey(KeyCode.Space) && isOnGround;
        public bool IsShoot => Input.GetKeyDown(KeyCode.E);
        
        private void Awake()
        {
            score = 0;
            rigidbodyPlayer = GetComponent<Rigidbody2D>();
            isFirstShot = true;
        }

        private void Start()
        {
            lastShootTime = Time.time;
            arrowSpawnPoint = transform.Find("ArrowSpawnPoint");
            anim = GetComponentInChildren<Animator>();
            flipPlayer = GetComponentInChildren<SpriteRenderer>();

            if (playerUi == null)
            {
                Debug.Log("playerUi is null");
            }
            else
            {
                onPlayerDying.AddListener(playerUi.Playerkilled);
                ShotArrowCoroutine = FirstShot();
            }
        }


        private void Update()
        {
            Walk();

            UpdateJump();

            UpdateShooting();
        }

        private void UpdateShooting()
        {
            bool runShootingAnimation = false;

            if (IsShoot)
            {
                if (HasPlayerCooldownExpired())
                {
                    StartCoroutine(ArrowGenerator());

                    if (isFirstShot)
                    {
                        playerUi.PlayersFirstShotWasFired();
                        isFirstShot = false;
                    }
                    runShootingAnimation = true;
                }
            }

            anim.SetBool("isShooting", runShootingAnimation);
        }

        private void UpdateJump()
        {
            bool runJumpingAnimation = false;

            if (IsJump)
            {
                runJumpingAnimation = true;
                rigidbodyPlayer.velocity = new Vector2(0, jump);
                isOnGround = false;
            }

            anim.SetBool("isJumping", runJumpingAnimation);
        }


        private IEnumerator FirstShot()
        {
            Debug.Log("in FirstShot");
            Debug.Log("this is not workig !! need to fix or del ");
            playerUi.PlayersFirstShotWasFired();

            ShotArrowCoroutine = ArrowGenerator();

            StartCoroutine(ShotArrowCoroutine);
            yield return null;
        }

        IEnumerator ArrowGenerator()
        {
            yield return new WaitForSeconds(0.3f);
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(0, 0, -90));
            arrow.GetComponent<ArrowShooting>().onKillingEnemy.AddListener(KillEnemy);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(60f, 3f);
        }

        private void KillEnemy()
        {
            score++;
            playerUi.Score = score;
        }

        private void Walk()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            rigidbodyPlayer.velocity = new Vector2(moveHorizontal * speed, rigidbodyPlayer.velocity.y);
            
            if (moveHorizontal > 0.01f)
            {
                flipPlayer.flipX = false;
                //anim.SetBool("isWalking", true);
            }
            else if (moveHorizontal < -0.01f)
            {
                flipPlayer.flipX = true;
                //anim.SetBool("isWalking", true);
            }
            else
            {
                //anim.SetBool("isWalking", false);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                PlayerDie();
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
            }
        }

        private void PlayerDie()
        {
            anim.SetBool("isDie", true);
            isCollidingWithEnemy = true;
            onPlayerDying?.Invoke();
            Destroy(gameObject, 0.3f);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                isCollidingWithEnemy = false;
            }
        }

        private bool HasPlayerCooldownExpired()
        {
            float timeSinceLastAction = Time.time - lastShootTime;
            bool canPlayrShootArrow = timeSinceLastAction >= shootCooldown && !flipPlayer.flipX;

            if (canPlayrShootArrow)
            {
                lastShootTime = Time.time; 
            }

            return canPlayrShootArrow;

        }
    }
}
