using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowShooting : MonoBehaviour
{
    public UnityEvent onKillingEnemy = new UnityEvent();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Enemy>().Waskilled())
            {
                onKillingEnemy?.Invoke();
            }

            Destroy(gameObject);
        }

        Destroy(gameObject, 2f);
    }
}
