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


    private void Start()
    {
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
}
