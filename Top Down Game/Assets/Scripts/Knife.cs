using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Vector3 throwDir;
    [SerializeField] float moveSpeed = 10f;

    float rotSpeed = 300f;

    enum State{
        Flying,
        Falling,
        Fallen,
    }

    State state;

    public void Setup(Vector3 throwDir){
        state = State.Flying;
        this.throwDir = throwDir;
    }

    void Update(){
        switch(state){
            case State.Flying:
            transform.position += throwDir * Time.deltaTime * moveSpeed;
            break;
            case State.Falling:
            transform.position -= throwDir * Time.deltaTime * moveSpeed/10;
            transform.Rotate(Vector3.forward * (rotSpeed * Time.deltaTime));
            rotSpeed -= 15f * Time.deltaTime;
            break;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>() != null){
            
        }
        else if(other.GetComponent<Knife>() != null){
        }
        else if(other.GetComponent<Player>() != null && state == State.Fallen){
            Player player = other.GetComponent<Player>();
            if(player.ammo >= 3){}
            else{
                player.ammo += 1;
                Destroy(this.gameObject);
            }
        }
        else{
            StartCoroutine(StartFalling());
        }
    }
    private IEnumerator StartFalling(){
        state = State.Falling;
        yield return new WaitForSeconds(1f);
        state = State.Fallen;
    }
}
