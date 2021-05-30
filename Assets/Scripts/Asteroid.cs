using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float spinSpeedMin = 15f;
    [SerializeField] float spinSpeedMax = 45f;
    float speedOfSpin = 180f;
    Rigidbody2D asteroid;
    void Start()
    {
        asteroid = GetComponent<Rigidbody2D>();
        asteroid.velocity = new Vector2(0, -movementSpeed);
        speedOfSpin = Random.Range(spinSpeedMin, spinSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
