using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    FunctionTimer functionTimer;
    [SerializeField]
    int damage = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Works.");
        if(other.GetComponent<Player>() != null){
            other.GetComponent<Player>().Damage(transform.GetComponentInParent<Enemy>(), damage);
        }
        if(other.GetComponent<Enemy>() != null){
            other.GetComponent<Enemy>().Damage(transform.GetComponentInParent<Player>(), damage);
            Debug.Log("Enemy hit.");
        }
    }
}
