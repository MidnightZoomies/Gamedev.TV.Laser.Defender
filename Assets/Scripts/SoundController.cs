﻿using System.Collections;
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

    public void EnemyShot()
    {
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, enemyShootSoundVolume);
    }

    public void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, enemyDeathSFXVolume);
    }

    public void PlayerShot()
    {
        AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, playerShootSoundVolume);
    }

    public void PlayerDeath()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathSFXVolume);
    }
}