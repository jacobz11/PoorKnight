using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        Destroy(gameObject, 2f);
    }
}
