using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("0 = Shield", order = 0)]
    [Space(-10, order = 1)]
    [Header("1 = TBD", order = 2)]
    [Space(-10, order = 3)]
    [Header("2 = TBD", order = 4)]
    [Space(10, order = 5)]
    [SerializeField] int powerUpID;
    PowerUpController powerUpController;
    SoundController soundController;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            powerUpController = FindObjectOfType<PowerUpController>();
            soundController = FindObjectOfType<SoundController>();
            soundController.PowerUp();
            switch (powerUpID)
            {
                case 0:
                    powerUpController.ShieldPowerUp();
                    break;
                    //case 1:
            }
            Destroy(gameObject);
        }
    }
}
