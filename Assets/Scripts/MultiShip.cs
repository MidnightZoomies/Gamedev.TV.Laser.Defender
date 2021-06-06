using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShip : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] int weaponType = 0;

    Vector3 dualLaserOffsetRight;
    Vector3 dualLaserOffsetLeft;

    Player player;
    Coroutine firingCoroutine;
    bool firingToggle = false;
    void Start()
    {
        player = FindObjectOfType<Player>();
        weaponType = player.MultiShipWeapon();
        dualLaserOffsetRight = new Vector3(0.25f, 0, 0);
        dualLaserOffsetLeft = new Vector3(-0.25f, 0, 0);
    }

    void Update()
    {
        weaponType = player.MultiShipWeapon();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && !firingToggle)
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
            if (weaponType == 0)
            {
                GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
            else if (weaponType == 1)
            {
                GameObject laserRight = Instantiate(playerLaser, transform.position + dualLaserOffsetRight, Quaternion.identity);
                GameObject laserLeft = Instantiate(playerLaser, transform.position + dualLaserOffsetLeft, Quaternion.identity);
                laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
            else if (weaponType == 2)
            {
                GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                GameObject laserRight = Instantiate(playerLaser, transform.position, Quaternion.Euler(Vector3.forward * -15f));
                GameObject laserLeft = Instantiate(playerLaser, transform.position, Quaternion.Euler(Vector3.forward * 15f));
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, laserSpeed);
                laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, laserSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }
    }
}
