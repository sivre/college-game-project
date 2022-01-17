using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Vector3 startingPosition;
    float minDistance = 2f;
    float maxDistance = 6f;

    GameObject playerGameObject;
    Transform player;
    Enemy enemy;
    float speed;
    float nextWaypointDistance = .5f;

    Path currentPath;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Character_Base character_Base;

    enum State{
        Roaming,
        Chasing,
    }
    State state;
    Vector3 target;

    void Awake(){
        enemy = GetComponent<Enemy>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        character_Base = GetComponent<Character_Base>();
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
    }

    void Start()
    {
        startingPosition = transform.position;
        state = State.Roaming;
        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb.position, target, OnPathComplete);
    }

    void Update(){
        speed = enemy.speed;
        switch(state){
            case State.Roaming:
                target = GetRoamingPos();
                FindTarget();
                break;
            case State.Chasing:
                target = player.position;
                reachedEndOfPath = true;

                float attackRange = 1f;
                if(Vector3.Distance(transform.position, player.position) < attackRange){
                    enemy.Attack((player.position - transform.position).normalized);
                }
                break;
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            currentPath = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate(){
        if(currentPath == null){
            return;
        }
        if(currentWaypoint >= currentPath.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        }
        else{
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)currentPath.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 movement = dir * speed;

        rb.velocity = movement;
        if(movement.x != 0){
            character_Base.CheckIfFlipCharacter(movement.x < 0);
        }

        float dist = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

        if(dist <  nextWaypointDistance){
            currentWaypoint++;
        }
    }

    Vector3 GetRoamingPos(){
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * Random.Range(minDistance, maxDistance);
    }

    void UpdatePath(){
        if (seeker.IsDone() && reachedEndOfPath){
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }

    void FindTarget(){
        float targetRange = 10f;
        if(Vector3.Distance(transform.position, player.position) < targetRange){
            state = State.Chasing;
        }
    }
}
