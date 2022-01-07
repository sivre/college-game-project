using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody2D rb;
    
    Vector2 movement;
    float horizontal = 1f;

    void Update() {
        MovementInput();
        SpriteRotation();
    }

    void FixedUpdate() {
        Vector2 playerMovement = movement * playerSpeed;
        rb.velocity = playerMovement;
        animator.SetFloat("Current Speed", Mathf.Abs(playerMovement.magnitude));
    }

    void MovementInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(x, y).normalized;
    }

    void SpriteRotation() {
        if(Input.GetAxisRaw("Horizontal") != 0){
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if(horizontal < 0){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else{
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
