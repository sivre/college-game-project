using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Player : MonoBehaviour
{
    public float defaultPlayerSpeed;
    float playerSpeed;
    float maxPlayerSpeed;
    public HealthSystem health;
    public int ammo;
    public int maxAmmo = 3;
    bool isInvulnerable = false;
    
    [SerializeField] Character_Base character;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform slash;
    [SerializeField] Transform knife;
    [SerializeField] UI ui;
    
    Vector2 movement;
    float horizontal = 1f;

    private enum State{
        Normal,
        Attacking,
        NoWeapon,
    }

    private State state;

    void Awake() {
        state = State.Normal;
        playerSpeed = defaultPlayerSpeed;
        ammo = maxAmmo;
        maxPlayerSpeed = playerSpeed * 1.5f;
        health = new HealthSystem(5);
    }

    void Update() {
        //States so you can't walk while attacking
        switch(state){
            case State.Normal:
        MovementInput();
        GetAttackDirectionAndAttack();

            break;

            case State.Attacking:
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player0 Attack")){
            break;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player0 Throw")){
            break;
        }
        else{
            state = State.Normal;
        }
            break;

            case State.NoWeapon:
            
        if(ammo == 0){
            if(playerSpeed > maxPlayerSpeed){
                playerSpeed = maxPlayerSpeed;
        }
        else{
            playerSpeed += 0.5f * Time.deltaTime;
        }
        MovementInput();
        }
        else if(ammo > 0){
            playerSpeed = defaultPlayerSpeed;
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

        //Saves last horizontal value so the character doesn't automatically flip around, but instead keeps the rotation
        if(Input.GetAxisRaw("Horizontal") != 0){
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        character.CheckIfFlipCharacter(horizontal < 0);
    }

    void GetAttackDirectionAndAttack(){
        if(Input.GetMouseButtonDown(0)){
            InstantiateAttack(GetMouseDirection(), "Player0 Attack", slash);
        }
        if(Input.GetMouseButtonDown(1)){
            if(ammo > 0){
            Vector3 throwDir = GetMouseDirection();
            Transform knifeTransform = GetInstantiateAttack(throwDir, "Player0 Throw", knife);

            knifeTransform.GetComponent<Knife>().Setup(throwDir);
            ammo -= 1;
            CheckAmmo();
            }
        }   
    }

    void CheckAmmo(){
        if(ammo < 0){
        ammo = 0;
        }
        else if(ammo > 3){
            ammo = 3;
        }
        if(ammo == 0){
            state = State.NoWeapon;
        }
    }


    void InstantiateAttack(Vector3 attackDir, string animName, Transform prefab){

        //State used to stop the character and movement while attacking
        state = State.Attacking;
        movement = Vector2.zero;
            
        horizontal = attackDir.x;
        character.CheckIfFlipCharacter(attackDir.x < 0);

        animator.Play(animName);

        //Calculates the direction where the attack will be instantiated
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        //The new Vector is used so the particle doesn't Instantiate below the player
        var Prefab = Instantiate(prefab, attackDir + transform.position + new Vector3(0f, transform.localScale.y/2.2f, 0f), rot);
        Prefab.transform.parent = gameObject.transform;
    }

    Transform GetInstantiateAttack(Vector3 attackDir, string animName, Transform prefab){

        //State used to stop the character and movement while attacking
        state = State.Attacking;
        movement = Vector2.zero;
            
        horizontal = attackDir.x;
        character.CheckIfFlipCharacter(attackDir.x < 0);

        animator.Play(animName);

        //Calculates the direction where the attack will be instantiated
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //The new Vector is used so the particle doesn't Instantiate below the player
        return Instantiate(prefab, attackDir + transform.position + new Vector3(0f, transform.localScale.y/2.2f, 0f), rot);
    }

    Vector3 GetMouseDirection(){
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 mouseDir = (mousePosition - transform.position).normalized;
        return mouseDir;
    }

    public void Damage(Enemy attacker, int damage){
        Vector3 attackerPos = transform.position;
        if(attacker != null){
            attackerPos = attacker.transform.position;
        }
        if(!isInvulnerable){
            health.Damage(damage);
            StartCoroutine(character.Invulnerable());
        }

        //sound
        //blood
    }

    IEnumerator Invulnerable(){
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }
}
