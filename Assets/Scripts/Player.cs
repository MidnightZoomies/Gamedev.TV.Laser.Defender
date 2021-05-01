using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] float moveSpeed = 15f;
    
    /*[SerializeField] float screenWidth = 10;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    */

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        /*float mousePos = Input.mousePosition.x / Screen.width * screenWidth; //the current position of the mouse in relation to the screenwidth
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y); //creating vector2 with current object location
        playerPos.x = Mathf.Clamp(mousePos, minX, maxX); //prevents paddle from going off the screen on the x-axis;
        transform.position = playerPos; //changing transform with vector2
        */

        /* var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(newXPos, newYPos);
        */

    }
}
