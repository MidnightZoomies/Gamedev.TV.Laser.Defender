using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{
    [SerializeField] float health = 25000f;
    SoundController soundController;

    [Header("Ship Explosion")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Game Score")]
    GameSession gameSession;
    [SerializeField] int enemyScore = 100;

    Boss boss;
    BossPathing bossPathing;
    BossPartGun bossPartGun;
    int damage;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        gameSession = FindObjectOfType<GameSession>();
        boss = FindObjectOfType<Boss>();
        bossPathing = FindObjectOfType<BossPathing>();
        bossPartGun = FindObjectOfType<BossPartGun>();
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
        damage = damageManager.GetDamage();
        health -= damage;
        if (damageManager.tag == "PlayerLaser")
        {
            damageManager.Hit();
        }
        if (health <= 0)
        {
            BossPartDeath();
        }
    }

    public void BossPartDeath()
    {
        gameSession.AddToScore(enemyScore);
        boss.BossPartDestroyed();
        bossPathing.BossPartDestroyed();
        bossPartGun.BossPartDestroyed();
        BossPartDeathEffects();
    }

    private void BossPartDeathEffects()
    {
        Destroy(gameObject);
        //add bigger explosion?
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        soundController.EnemyDeath();
    }
}
