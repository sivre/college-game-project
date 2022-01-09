using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Animator hearts;
    [SerializeField] List<Image> knives;
    Player player;


    void Start(){
        player = FindObjectOfType<Player>();
    }
    void Update(){
        CheckHealth();
        StartCoroutine(CheckAndChangeKnives());
    }

    private IEnumerator CheckAndChangeKnives(){
        if(player.ammo != 0){
            for(int i = 0; i < player.ammo; i++){
            knives[i].color = new Color(255, 255, 255, 255);
        }
    }
        int u = player.maxAmmo - player.ammo;
        if(player.ammo != player.maxAmmo){
            for(int i = 2; i >= player.ammo; i--){
                knives[i].color = new Color(0,0,0,255);
            }
        }
        yield return new WaitForSeconds(0.25f);
    }

    void CheckHealth(){
        hearts.SetInteger("Health", player.health.GetHealth());
    }
}
