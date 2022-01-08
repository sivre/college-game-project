using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Base : MonoBehaviour
{
    public void CheckIfFlipCharacter(bool condition){
        if(condition){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else{
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
