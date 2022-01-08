using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Vector3 throwDir;
    [SerializeField]
    float moveSpeed = 10f;

    public void Setup(Vector3 throwDir){
        this.throwDir = throwDir;
        Destroy(gameObject, 5f);
    }

    void Update(){
        transform.position += throwDir * Time.deltaTime * moveSpeed;
    }
}
