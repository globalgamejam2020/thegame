using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;


public class HumanController : MonoBehaviour {

    [SerializeField] private Vector2[] patrolPoints;
    [SerializeField] private float alertRadius = 10;
    [SerializeField] private Movement movement;
    bool canMove = true;

    void Start() {
        movement = GetComponent<Movement>();
    }

    void Update() {
        if(!movement.isMoving() && canMove) {
            canMove = false;
            movement.Move(Data.MovementDirection.NORTH);
        }
    }
}
