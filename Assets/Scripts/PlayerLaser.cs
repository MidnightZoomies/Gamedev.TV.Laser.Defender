using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TrashCollector")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
