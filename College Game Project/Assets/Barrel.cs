using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    int health = 1;

    [SerializeField]
   UnityEngine.Object destructableRef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ammo"))
        {
            health--;
            if(health <=0)
            {
                ExplodeThisGameObject();
            }
        }
    }
   private void ExplodeThisGameObject()
    {
        GameObject destructable = (GameObject) Instantiate (destructableRef);
        destructable.transform.position = transform.position;

        Destroy(gameObject);
    }

}