using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPrefab : MonoBehaviour
{
    public UnityEvent onObjectDestroyed = new UnityEvent(); // Create a new UnityEvent that will be invoked when the GameObject is destroyed

    void OnDestroy()
    {
        onObjectDestroyed.Invoke(); // Invoke the UnityEvent when the GameObject is destroyed
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject);
        }
    }
}