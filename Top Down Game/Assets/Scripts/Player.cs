using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Player : MonoBehaviour
{
    public float playerSpeed;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    GameObject slash;
    
    Vector2 movement;
    float horizontal = 1f;

    private enum State{
        Normal,
        Attacking,
    }

    private State state;

    void Awake() {
        state = State.Normal;
    }

    void Update() {
        switch(state){
            case State.Normal:
        MovementInput();
        GetAttackDirectionAndAttack();
            break;

            case State.Attacking:
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player0 Attack")){
            break;
        }
        else{
            state = State.Normal;
        }
            break;
        }
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
        if(Input.GetAxisRaw("Horizontal") != 0){
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        CheckIfFlipCharacter(horizontal < 0);
    }

    void GetAttackDirectionAndAttack(){
        if(Input.GetMouseButtonDown(0)){

            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 attackDir = (mousePosition - transform.position).normalized;

            state = State.Attacking;
            movement = Vector2.zero;
            
            horizontal = attackDir.x;
            CheckIfFlipCharacter(attackDir.x < 0);
            animator.Play("Player0 Attack");

            float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

            Instantiate(slash, attackDir + transform.position - new Vector3(0f, transform.localScale.y/2.2f, 0f), rot);
        }
    }

    void CheckIfFlipCharacter(bool condition){
        if(condition){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else{
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
