using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config perams
    [Header("Health")]
    [SerializeField] int playerHealth = 500;
    [SerializeField] int healthPowerUp = 500;

    [Header("Movement")]
    //[SerializeField] float moveSpeed = 100f;
    float padding = 0.5f;

    [Header("Laser")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header ("Player Death")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration = 1f;

    SoundController soundController;

    Coroutine firingCoroutine;
    bool firingToggle = false;
    int fireType = 0;

    Vector3 dualLaserOffsetRight;
    Vector3 dualLaserOffsetLeft;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    private Vector3 mousePosition;
    
    /*private Vector3 mouseOffSet;
    private Rigidbody2D rb;
    private Vector2 direction;*/


    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        soundController = FindObjectOfType<SoundController>();
        SetUpMoveBoundaries();
        dualLaserOffsetRight = new Vector3(0.25f, 0, 0);
        dualLaserOffsetLeft = new Vector3(-0.25f, 0, 0);
        //mouseOffSet = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !firingToggle)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            firingToggle = true;
        }
        if (Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            firingToggle = false;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (fireType == 0)
            {
                GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                soundController.PlayerShot();
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
            else if (fireType == 1)
            {
                GameObject laserRight = Instantiate(playerLaser, transform.position + dualLaserOffsetRight, Quaternion.identity);
                GameObject laserLeft = Instantiate(playerLaser, transform.position + dualLaserOffsetLeft, Quaternion.identity);
                laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                soundController.PlayerShot();
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
            else if (fireType == 2)
            {
                GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                GameObject laserRight = Instantiate(playerLaser, transform.position, Quaternion.Euler(Vector3.forward * -15f));
                GameObject laserLeft = Instantiate(playerLaser, transform.position, Quaternion.Euler(Vector3.forward * 15f));
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, laserSpeed);
                laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, laserSpeed);
                soundController.PlayerShot();
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newXPos = Mathf.Clamp(mousePosition.x, xMin, xMax);
        var newYPos = Mathf.Clamp(mousePosition.y, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        //direction = (mousePosition - transform.position).normalized;
        //rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        /*var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser" || other.tag == "Enemy")
        {
            DamageManager damageManager = other.gameObject.GetComponent<DamageManager>();
            if (!damageManager) {return;} //if damageManager is null, does not progress.
            ProcessHit(damageManager);
        }
        else if (other.tag == "Asteroid")
        {
            PlayerDeath();
        }
    }

    private void ProcessHit(DamageManager damageManager)
    {
        playerHealth -= damageManager.GetDamage();
        if (damageManager.tag == "EnemyLaser")
        {
            damageManager.Hit();
        }
        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        soundController.PlayerDeath();
    }

    public int UpdatePlayerHealth()
    {
        return playerHealth;
    }

    public void HealthPowerUp()
    {
        playerHealth = healthPowerUp;
    }

    public void FireType(int weapon)
    {
        fireType = weapon;
    }

    public int MultiShipWeapon()
    {
        return fireType;
    }
}
