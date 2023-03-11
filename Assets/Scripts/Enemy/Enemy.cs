using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onObjectDestroyed = new UnityEvent();
    private byte LifePoints = 3;
    private Rigidbody2D rigidbody;
    private float speed;
    private bool isToRight = true;
    public float Speed { get => speed * Direction;  set => speed = value;  }
    public float Direction
    {
        get => isToRight ? -1 : 1;
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody == null)
        {
            Debug.LogError("Enemy rigidbody is null ");
        }
    }

    void OnDestroy()
    {
        onObjectDestroyed.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (Waskilled())
            {
                Destroy(gameObject);
            }
            else
            {
                LifePoints--;
            }
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        this.rigidbody.velocity = new Vector2(Speed , this.rigidbody.velocity.y);
    }

    internal bool Waskilled()
    {
        return LifePoints -1 <= 0;
    }

    internal void PlayerKilled()
    {
        Destroy(gameObject);
    }
}