using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] [Range(0, 1)] float enemyDeathSFXVolume = 0.7f;
    [SerializeField] AudioClip enemyShootSound;
    [SerializeField] [Range(0, 1)] float enemyShootSoundVolume = 0.25f;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] [Range(0, 1)] float playerDeathSFXVolume = 0.7f;
    [SerializeField] AudioClip playerShootSound;
    [SerializeField] [Range(0, 1)] float playerShootSoundVolume = 0.25f;
    [SerializeField] AudioClip asteroidDestructionSFX;
    [SerializeField] [Range(0, 1)] float asteroidDestructionSFXVolume = 0.7f;
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] [Range(0, 1)] float powerUpSFXVolume = 0.7f;

    void Awake()
    {
        SetUpSingleton();
    }

    void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void EnemyShot()
    {
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, enemyShootSoundVolume);
    }

    public void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, enemyDeathSFXVolume);
    }

    public IEnumerator BossDeath()
    {
        Debug.Log("Boom!");
        for (int i = 3; i > 0; i--)
        {
            AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, enemyDeathSFXVolume);
            yield return new WaitForSeconds(enemyDeathSFX.length);
        }
    }

    public void PlayerShot()
    {
        AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, playerShootSoundVolume);
    }

    public void PlayerDeath()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathSFXVolume);
    }

    public void AsteroidDestruction()
    {
        AudioSource.PlayClipAtPoint(asteroidDestructionSFX, Camera.main.transform.position, asteroidDestructionSFXVolume);
    }

    public void PowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position, powerUpSFXVolume);
    }

    public void BossDeathTrigger()
    {
        StartCoroutine(BossDeath());
    }
}
