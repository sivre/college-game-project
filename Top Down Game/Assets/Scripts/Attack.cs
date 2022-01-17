using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    FunctionTimer functionTimer;
    [SerializeField] bool canAttackEnemy;
    [SerializeField] bool canAttackPlayer;
    [SerializeField]
    int damage = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>() != null && canAttackPlayer){
            other.GetComponent<Player>().Damage(damage);
        }
        if(other.GetComponent<Enemy>() != null && canAttackEnemy){
            other.GetComponent<Enemy>().Damage(damage);
        }
    }
}
