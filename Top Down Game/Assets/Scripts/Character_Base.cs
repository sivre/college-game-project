using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Base : MonoBehaviour
{
    public bool isInvulnerable = false;

    public IEnumerator Invulnerable(){
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }

    public void CheckIfFlipCharacter(bool condition){
        if(condition){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else{
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void InstantiateAttack(Vector3 attackDir, Transform prefab){
        //Calculates the direction where the attack will be instantiated
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //The new Vector is used so the particle doesn't Instantiate below the player
        Instantiate(prefab, attackDir + transform.position + new Vector3(0f, transform.localScale.y/2.2f, 0f), rot);
    }
}
