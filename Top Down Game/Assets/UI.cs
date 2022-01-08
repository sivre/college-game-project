using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Animator hearts;
    Player player;

    void Start(){
        player = FindObjectOfType<Player>();
    }
    void Update(){
        CheckHealth();
    }

    void CheckHealth(){
        hearts.SetInteger("Health", player.health.GetHealth());
    }
}
