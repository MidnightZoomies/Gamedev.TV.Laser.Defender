using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    //Do I need a singleton?

    [SerializeField] GameObject shieldObject;
    bool shieldActive = false;
    Player player;
    Shield shield;
    [SerializeField] List<GameObject> powerUps;
    [SerializeField] int powerUpChanceMeasure;
    [SerializeField] int asteroidPowerUpMeasure;
    float powerUpSpeed = -1f;
    int powerUpTypeRandom;
    int powerUpTypeRandomMax;
    int powerUpChanceRandom;
    bool multiShipRightCheck = false;
    bool multiShipLeftCheck = false;
    Vector3 multiShipRightOffset = new Vector3(2, -0.5f, 0);
    Vector3 multiShipLeftOffset = new Vector3(-2, -0.5f, 0);
    [SerializeField] GameObject multiShipRightObject;
    [SerializeField] GameObject multiShipLeftObject;
    WeaponController weaponControllerScript;
    [SerializeField] GameObject weaponController;

    private void Start()
    {
        powerUpTypeRandomMax = powerUps.Count;
        player = FindObjectOfType<Player>();
    }

    public void ShieldPowerUp()
    {
        if (!shieldActive)
        {
            Instantiate(shieldObject, player.transform.position, Quaternion.identity);
            shieldActive = true;
        }
        else
        {
            shield = FindObjectOfType<Shield>();
            shield.RechargeShield();
        }
    }

    public void ShieldStatus()
    {
        shieldActive = false;
    }

    public void PowerUp(Vector3 obj)
    {
        powerUpChanceRandom = Random.Range(0, 101);
        if (powerUpChanceRandom <= powerUpChanceMeasure)
        {
            powerUpTypeRandom = Random.Range(0, powerUpTypeRandomMax);
            GameObject powerUpInstance = Instantiate(powerUps[powerUpTypeRandom], obj, Quaternion.identity);
            powerUpInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, powerUpSpeed);
        }
    }

    public void PowerUpAsteroid(Vector3 obj)
    {
        powerUpChanceRandom = Random.Range(0, 101);
        if (powerUpChanceRandom <= asteroidPowerUpMeasure)
        {
            powerUpTypeRandom = Random.Range(0, powerUpTypeRandomMax - 1);
            GameObject powerUpInstance = Instantiate(powerUps[powerUpTypeRandom], obj, Quaternion.identity);
            powerUpInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, powerUpSpeed);
        }
    }

    public void MultiShip()
    {
        if (multiShipLeftCheck || multiShipRightCheck)
        {
            if (!multiShipLeftCheck)
            {
                GameObject multiShipLeft = Instantiate(multiShipLeftObject, player.transform.position + multiShipLeftOffset, Quaternion.identity);
                multiShipLeft.transform.SetParent(player.GetComponent<Transform>());
                multiShipLeftCheck = true;
            }
            if (!multiShipRightCheck)
            {
                GameObject multiShipRight = Instantiate(multiShipRightObject, player.transform.position + multiShipRightOffset, Quaternion.identity);
                multiShipRight.transform.SetParent(player.GetComponent<Transform>());
                multiShipRightCheck = true;
            }
        }
        else
        {
            GameObject multiShipRight = Instantiate(multiShipRightObject, player.transform.position + multiShipRightOffset, Quaternion.identity);
            GameObject multiShipLeft = Instantiate(multiShipLeftObject, player.transform.position + multiShipLeftOffset, Quaternion.identity);
            multiShipRight.transform.SetParent(player.GetComponent<Transform>());
            multiShipLeft.transform.SetParent(player.GetComponent<Transform>());
            multiShipLeftCheck = true;
            multiShipRightCheck = true;
        }
    }

    public void MultiShipLeftDeath()
    {
        multiShipLeftCheck = false;
    }

    public void MultiShipRightDeath()
    {
        multiShipRightCheck = false;
    }

    public void WeaponController()
    {
        if (GameObject.Find("WeaponController(Clone)"))
        {
            weaponControllerScript = FindObjectOfType<WeaponController>();
            weaponControllerScript.WeaponRandom();
        }
        else
        {
            Instantiate(weaponController, transform.position, transform.rotation);
        }
    }
}
