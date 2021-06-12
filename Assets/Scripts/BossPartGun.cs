using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartGun : MonoBehaviour
{
    [Header("Enemy Laser")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float laserSpeed = -5f;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    SoundController soundController;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        soundController.EnemyShot();
    }

    public void BossPartDestroyed()
    {
        laserSpeed -= 1f;
    }
}
