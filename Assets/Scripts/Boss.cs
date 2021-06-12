using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float health = 100000f;
    SoundController soundController;

    [Header("Enemy Laser")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float laserSpeed = -20f;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enrageValue = 0.9f;
    //[SerializeField] Transform target;

    [Header("Ship Explosion")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Game Score")]
    GameSession gameSession;
    [SerializeField] int enemyScore = 100;

    Level level;
    PowerUpController powerUpController;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        gameSession = FindObjectOfType<GameSession>();
        level = FindObjectOfType<Level>();
        powerUpController = FindObjectOfType<PowerUpController>();
        powerUpController.IncreaseAsteroidDrops();
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
        GameObject laserRight = Instantiate(enemyLaser, transform.position, Quaternion.Euler(Vector3.forward * -5f));
        GameObject laserLeft = Instantiate(enemyLaser, transform.position, Quaternion.Euler(Vector3.forward * 5f));
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, laserSpeed);
        laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, laserSpeed);
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
            BossDeath();
        }
    }

    private void BossDeath()
    {
        gameSession.AddToScore(enemyScore);
        BossDeathEffects();
    }

    private void BossDeathEffects()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        soundController.BossDeathTrigger();
        level.LoadWinScreen();
    }

    public void BossPartDestroyed()
    {
        maxTimeBetweenShots = maxTimeBetweenShots * enrageValue;
        laserSpeed -= 1;
    }
}
