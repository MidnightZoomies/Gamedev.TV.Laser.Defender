using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TrashCollector")
        {
            Destroy(gameObject);
        }
        /*else if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }*/
    }
}
