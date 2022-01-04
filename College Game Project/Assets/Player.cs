using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    [SerializeField] int health  = 2;
    Vector2 motionVector;
   
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        motionVector = new Vector2(
             Input.GetAxisRaw("Horizontal"),
             Input.GetAxisRaw("Vertical")
             );
     
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            speed += 5f;
            Debug.Log("stop touching me");
        }

        else if (collision.gameObject.CompareTag("HP"))
        {
            health += 1;
        }
    }
}