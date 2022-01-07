using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MeleeAttack : MonoBehaviour
{
    void GetAttackDirectionAndAttack(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 attackDir = (mousePosition - transform.position).normalized;
            
        }
    }
}
