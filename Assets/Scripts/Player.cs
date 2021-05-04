using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float padding = 0.5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    
    /*[SerializeField] float screenWidth = 10;
    [SerializeField] float minX;
    [SerializeField] float maxX;

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 100f;
    */

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();

        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        /*mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);*/

        /* Cant seem to get this to work right
        float mousePos = Input.mousePosition.x / Screen.width * screenWidth; //the current position of the mouse in relation to the screenwidth
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y); //creating vector2 with current object location
        playerPos.x = Mathf.Clamp(mousePos, minX, maxX); //prevents paddle from going off the screen on the x-axis;
        transform.position = playerPos; //changing transform with vector2
        */

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}
