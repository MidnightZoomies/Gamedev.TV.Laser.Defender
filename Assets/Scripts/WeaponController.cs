using System.Collections;
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
        weaponRandom = Random.Range(0, 2);
        switch (weaponRandom)
        {
            case 0:
                player.DualLaser();
                break;
            case 1:
                player.SpreadFire();
                break;
        }
    }

    void Update()
    {
        weaponDuration -= 1f * Time.deltaTime;
        if (weaponDuration <= 0)
        {
            player.NormalFire();
            Destroy(gameObject);
        }
    }
}
