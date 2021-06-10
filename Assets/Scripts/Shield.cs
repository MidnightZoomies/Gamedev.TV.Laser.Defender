using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Shield is timed in Update
    //Shield has health, changes color at certain damage levels
    //May need to add damage script/values to Asteroids

    [Header("Shield Stats")]
    [SerializeField] float shieldDuration = 360f;
    [SerializeField] int shieldDurationDefault = 360;
    [SerializeField] int shieldHealth = 500;
    [SerializeField] int shieldHealthDefault = 500;
    SpriteRenderer shieldColor;

    //References
    Player player;
    DamageManager damageManager;
    PowerUpController powerUpController;

    void Start()
    {
        player = FindObjectOfType<Player>();
        damageManager = FindObjectOfType<DamageManager>();
        powerUpController = FindObjectOfType<PowerUpController>();
        shieldColor = gameObject.GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        gameObject.transform.position = player.transform.position;
        shieldDuration -= 1f * Time.deltaTime;
        if (shieldDuration <= 0)
        {
            DestroyShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser" || other.tag == "Enemy")
        {
            DamageManager damageManager = other.gameObject.GetComponent<DamageManager>();
            if (!damageManager) { return; } //if damageManager is null, does not progress.
            ProcessHit(damageManager);
        }
        else if (other.tag == "Asteroid")
        {
            DestroyShield();
        }
    }

    private void ProcessHit(DamageManager damageManager)
    {
        shieldHealth -= damageManager.GetDamage();
        if (damageManager.tag == "EnemyLaser")
        {
            damageManager.Hit();
        }
        ShieldDamage();
    }

    void ShieldDamage()
    {
        if (shieldHealth >= 101 && shieldHealth <= 300) //Shield colors based on health
        {
            shieldColor.color = new Color(1, 1, 1, 0.50f);
        }
        else if (shieldHealth >= 1 && shieldHealth <= 100)
        {
            shieldColor.color = new Color(1, 1, 1, 0.25f);
        }
        if (shieldHealth <= 0)
        {
            DestroyShield();
        }
    }

    public void RechargeShield()
    {
        shieldHealth = shieldHealthDefault;
        shieldDuration = shieldDurationDefault;
        shieldColor.color = new Color(1, 1, 1, 1);
    }

    void DestroyShield()
    {
        powerUpController.ShieldStatus();
        Destroy(gameObject);
    }
}
