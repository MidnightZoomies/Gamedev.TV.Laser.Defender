using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("0 = Shield", order = 0)]
    [Space(-10, order = 1)]
    [Header("1 = Health", order = 2)]
    [Space(-10, order = 3)]
    [Header("2 = Bomb", order = 4)]
    [Space(-10, order = 5)]
    [Header("3 = Weapon", order = 6)]
    [Space(10, order = 7)]
    [SerializeField] int powerUpID;

    PowerUpController powerUpController;
    SoundController soundController;
    Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "MultiShipLeft" || other.tag == "MultiShipRight")
        {
            powerUpController = FindObjectOfType<PowerUpController>();
            soundController = FindObjectOfType<SoundController>();
            soundController.PowerUp();
            switch (powerUpID)
            {
                case 0:
                    powerUpController.ShieldPowerUp();
                    break;
                case 1:
                    player = FindObjectOfType<Player>();
                    player.HealthPowerUp();
                    break;
                case 2:
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Enemy>().EnemyDeath();
                    }
                    break;
                case 3:
                    powerUpController.WeaponController();
                    break;
                case 4:
                    {
                        powerUpController = FindObjectOfType<PowerUpController>();
                        powerUpController.MultiShip();
                        break;
                    }
            }
            Destroy(gameObject);
        }
    }
}
