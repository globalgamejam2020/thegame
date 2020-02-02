using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;
using Data;

public class HumanController : MonoBehaviour {

    [SerializeField] private UnityEngine.Vector3[] patrolPoints;
    private int nextPatrolPointIndex = 0;
    [SerializeField] private float alertRadius = 10;
    [SerializeField] private Movement movement;

    void Start() {
        movement = GetComponent<Movement>();
    }

    void Update() {
        if(!movement.isMoving()) {
            planMovement();
        } else if(patrolPoints[nextPatrolPointIndex] == this.transform.position) {
            nextPatrolPointIndex ++;
            if(nextPatrolPointIndex == patrolPoints.Length)
                nextPatrolPointIndex = 0;
        }
    }

    void planMovement() {
        UnityEngine.Vector2 direction = UnityEngine.Vector2.zero;
        UnityEngine.Vector3 destination = patrolPoints[nextPatrolPointIndex];
        
        if(destination.y < this.transform.position.y) {
            direction.y += -1f;
        } else if (destination.y > this.transform.position.y) {
            direction.y += 1f;
        }

        if(destination.x < this.transform.position.x) {
            direction.x += -1f;
        } else if (destination.x > this.transform.x) {
            direction.x += 1f;
        }
        movement.Move(direction);
    }
}
