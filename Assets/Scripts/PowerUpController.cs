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
    float powerUpSpeed = -1f;
    int powerUpTypeRandom;
    int powerUpTypeRandomMax;
    int powerUpChanceRandom;

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
}
