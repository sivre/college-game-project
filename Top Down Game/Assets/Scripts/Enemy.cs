using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem health;
    bool isDead = false;
    [SerializeField] int maxHealth = 5;
    [SerializeField] int currentHealth;
    [SerializeField] float defaultSpeed = 1f;
    public float speed;
    Character_Base character_Base;
    [SerializeField] Transform attackPrefab;
    FunctionTimer functionTimer;
    MaterialTintColor materialTintColor;
    Animator animator;
    [SerializeField] string deathAnimation;
    [SerializeField] float rotationOffset = 0f;
    [SerializeField] float positionOffset = 0f;

    enum State{
        Walking,
        Attacking,
        Dead,
    }
    State state;

    void Awake(){
        health = new HealthSystem(maxHealth);
        character_Base = GetComponent<Character_Base>();
        materialTintColor = GetComponent<MaterialTintColor>();
        animator = GetComponent<Animator>();
        state = State.Walking;
        speed = defaultSpeed;
    }

    public void Attack(Vector3 playerPos){
        if(state != State.Attacking){
            StartCoroutine(AttackTimer(playerPos));
        }
    }
    IEnumerator AttackTimer(Vector3 playerPos){
        state = State.Attacking;
        materialTintColor.SetTintColor(new Color(1,0,1,1f));
        yield return new WaitForSeconds(0.4f);
        materialTintColor.SetTintColor(new Color(1,0,1,1f));
        yield return new WaitForSeconds(0.4f);
        character_Base.InstantiateAttack(playerPos, attackPrefab, rotationOffset, positionOffset);
        state = State.Walking;
    }
    void Update(){
        switch(state){
            case State.Walking:
            if(speed == 0){
                speed = defaultSpeed;
            }
            IsDead();
            break;
            case State.Attacking:
            if(speed != 0){
                speed = 0;
            }
            IsDead();
            break;
            case State.Dead:
            if(speed != 0){
                speed = 0;
            }
            break;
        }

    }

    public void Damage(int damage){
        Vector3 attackerPos = transform.position;
        if(!character_Base.isInvulnerable){
            health.Damage(damage);
            StartCoroutine(character_Base.Invulnerable());
            materialTintColor.SetTintColor(new Color(1, 0, 0, 1f));
        }

        //sound
        //blood
    }

    void IsDead(){
        if(health.GetHealth() == 0 && !isDead){
            isDead = true;
            animator.Play(deathAnimation);
            state = State.Dead;
        }
    }
}
