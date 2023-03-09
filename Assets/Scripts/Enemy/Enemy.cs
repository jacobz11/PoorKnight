using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onObjectDestroyed = new UnityEvent();
    private byte LifePoints = 3;

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
    }

    internal bool Waskilled()
    {
        return LifePoints -1 <= 0;
    }
}