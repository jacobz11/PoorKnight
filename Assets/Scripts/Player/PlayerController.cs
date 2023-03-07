using System;
using UnityEngine;

namespace Assets.player
{
    internal class PlayerController : MonoBehaviour
    {
        public bool isCollidingWithEnemy;
        public bool isOnGround = true;

        private Rigidbody2D m_Rigidbody;
        private float m_Speed = 20;
        public GameObject arrowPrefab;
        private Transform arrowSpawnPoint;
        private SpriteRenderer flipPlayer;
        private Animator anim;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            arrowSpawnPoint = transform.Find("ArrowSpawnPoint");
            anim = GetComponentInChildren<Animator>();
            flipPlayer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            Walk();
            if (Input.GetKey(KeyCode.Space) && isOnGround)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                InstantiateNewArrows();
            }
        }

        private void InstantiateNewArrows()
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(0, 0, -90));
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(50f, 0f);
            //EulerRotation(0, 0, 11)
        }

        private void Walk()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            m_Rigidbody.velocity = new Vector2(moveHorizontal * m_Speed, m_Rigidbody.velocity.y);

            if (moveHorizontal > 0.01f)
            {
                flipPlayer.flipX = false;
                anim.SetBool("isWalking", true);
            }
            else if(moveHorizontal < -0.01f)
            {
                flipPlayer.flipX = true;
                anim.SetBool("isWalking", true);
            }
            else if(moveHorizontal == 0)
            {
                anim.SetBool("isWalking", false);
            }
        }

        private void Jump()
        {
            m_Rigidbody.velocity = new Vector2(0, 6);
            isOnGround = false;
        }

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                isCollidingWithEnemy = true;
                Destroy(gameObject, 0.3f);
            }
            if (collision.gameObject.CompareTag("Ground"))
                isOnGround = true;
            else isOnGround = false;
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                isCollidingWithEnemy = false;
            }
        }
    }
}
