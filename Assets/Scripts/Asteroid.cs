using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float spinSpeedMin = 15f;
    [SerializeField] float spinSpeedMax = 45f;
    float speedOfSpin = 180f;
    [SerializeField] int asteroidHealth = 1000;
    Rigidbody2D asteroid;
    DamageManager damageManager;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration = 1f;
    SoundController soundController;

    void Start()
    {
        damageManager = FindObjectOfType<DamageManager>();
        soundController = FindObjectOfType<SoundController>();
        asteroid = GetComponent<Rigidbody2D>();
        asteroid.velocity = new Vector2(0, -movementSpeed);
        speedOfSpin = Random.Range(spinSpeedMin, spinSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TrashCollector")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "PlayerLaser")
        {
            DamageManager damageManager = collision.gameObject.GetComponent<DamageManager>();
            if (!damageManager) { return; } //if damageManager is null, does not progress.
            ProcessHit(damageManager);
        }
    }

    private void ProcessHit(DamageManager damageManager)
    {
        asteroidHealth -= damageManager.GetDamage();
        if (damageManager.tag == "PlayerLaser")
        {
            damageManager.Hit();
        }
        if (asteroidHealth <= 0)
        {
            DestroyAsteroid();
        }
    }

    private void DestroyAsteroid()
    {
        Destroy(gameObject);
        PowerUpSpawn();
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        soundController.AsteroidDestruction();
    }

    private void PowerUpSpawn()
    {
        Vector3 currentPos = transform.position;
        PowerUpController powerUpController = FindObjectOfType<PowerUpController>();
        powerUpController.PowerUpAsteroid(currentPos);
    }
}
