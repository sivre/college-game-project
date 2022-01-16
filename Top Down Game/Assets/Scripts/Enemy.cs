using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem health;
    [SerializeField] int maxHealth = 5;
    [SerializeField] int currentHealth;
    public float speed = 1f;
    Character_Base character_Base;
    [SerializeField] Transform attackPrefab;
    FunctionTimer functionTimer;

    void Awake(){
        health = new HealthSystem(maxHealth);
        character_Base = GetComponent<Character_Base>();
    }

    public IEnumerator Attack(Vector3 playerPos){
        character_Base.InstantiateAttack(playerPos, attackPrefab);
        yield return new WaitForSeconds(1.5f); // doesn't work at delaying, need fix
    }

    void Update(){
        IsDead();
        currentHealth = health.GetHealth();
    }

    public void Damage(Player player, int damage){
        Vector3 attackerPos = transform.position;
        if(player != null){
            attackerPos = player.transform.position;
        }
        if(!character_Base.isInvulnerable){
            health.Damage(damage);
            StartCoroutine(character_Base.Invulnerable());
        }

        //sound
        //blood
    }

    void IsDead(){
        if(health.GetHealth() == 0){
            Destroy(gameObject);
        }
    }
}
