﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] float weaponDurationMax = 30f;
    int weaponRandom;
    float weaponDuration;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        WeaponRandom();
    }

    public void WeaponRandom()
    {
        weaponDuration = weaponDurationMax;
        weaponRandom = Random.Range(1, 3);
        player.FireType(weaponRandom);
    }

    void Update()
    {
        weaponDuration -= 1f * Time.deltaTime;
        if (weaponDuration <= 0)
        {
            player.FireType(0);
            Destroy(gameObject);
        }
    }
}
