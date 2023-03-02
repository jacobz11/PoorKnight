using System;
using UnityEngine;

namespace Assets.player
{
    internal class PlayerMovement : MonoBehaviour
    {
        private const byte k_Jump = GameKeyboardControls.k_Jump;
        private const byte k_Left = GameKeyboardControls.k_Left;
        private const byte k_Right = GameKeyboardControls.k_Right;
        private const byte k_Forwar = GameKeyboardControls.k_Power;
        private const byte k_Back = GameKeyboardControls.k_Back;
        private const byte k_Power = GameKeyboardControls.k_Power;
        public bool isCollidingWithEnemy;
        public bool isOnGround = true;

        private const bool k_MoveToTheRight = true;
        private const bool k_MoveToTheLeft = false;

        private GameKeyboardControls m_Controls;
        private Rigidbody2D m_Rigidbody;
        private float m_Speed = 20;
        public GameObject arrowPrefab;
        private Transform arrowSpawnPoint;

        private Animator anim;

        private void Awake()
        {
            m_Controls = new GameKeyboardControls();
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            arrowSpawnPoint = transform.Find("ArrowSpawnPoint");
            anim = GetComponentInChildren<Animator>();
        }

        [Obsolete]
        private void Update()
        {
            walk();
            if (m_Controls[k_Jump].Press&&isOnGround)
            {
                jump();
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.EulerRotation(0, 0, 11));
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(50f, 0f);
            }
        }

        private void walk()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            m_Rigidbody.velocity = new Vector2(moveHorizontal * m_Speed, m_Rigidbody.velocity.y);

            if (moveHorizontal > 0.01f)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                anim.SetBool("isWalking", true);
            }
            else if(moveHorizontal < -0.01f)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                anim.SetBool("isWalking", true);
            }
            else if(moveHorizontal==0)
            {
                anim.SetBool("isWalking", false);
            }
        }

        private void jump()
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
