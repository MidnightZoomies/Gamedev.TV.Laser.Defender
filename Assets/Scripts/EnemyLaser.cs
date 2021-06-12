using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TrashCollector" || collision.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
