using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    SoundController soundController;

    [Header("Enemy Laser")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float laserSpeed = -20f;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Ship Explosion")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Game Score")]
    GameSession gameSession;
    [SerializeField] int enemyScore = 100;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        gameSession = FindObjectOfType<GameSession>();
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
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        soundController.EnemyShot();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerLaser")
        {
            DamageManager damageManager = other.gameObject.GetComponent<DamageManager>();
            if (!damageManager) { return; } //if damageManager is null, does not progress.
            ProcessHit(damageManager);
        }
        else if (other.tag == "Player")
        {
            EnemyDeath();
        }
    }

    private void ProcessHit(DamageManager damageManager)
    {
        health -= damageManager.GetDamage();
        if (damageManager.tag == "PlayerLaser")
        {
            damageManager.Hit();
        }
        if (health <= 0)
        {
            EnemyDeath();
            
        }
    }

    private void EnemyDeath()
    {
        gameSession.AddToScore(enemyScore);
        EnemyDeathEffects();
    }

    /*private void AddToScoreOnDeath()
    {
        if (gameObject.name == "Fast Enemy(Clone)")
        {
            gameSession.AddToScore(fastEnemyScore);
        }
        else if (gameObject.name == "Slow Enemy(Clone)")
        {
            gameSession.AddToScore(slowEnemyScore);
        }
    }*/

    private void EnemyDeathEffects()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        soundController.EnemyDeath();
    }
}
