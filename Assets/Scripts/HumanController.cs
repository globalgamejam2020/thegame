using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;
using Data;

public class HumanController : MonoBehaviour {

    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private int nextPatrolPointIndex = 0;
    [SerializeField] private float alertRadius = 10;
    [SerializeField] private Movement movement;

    void Start() {
        movement = GetComponent<Movement>();
    }

    void Update() {
        if(patrolPoints.Length != 0) {
            if(patrolPoints[nextPatrolPointIndex].transform.position == this.transform.position) {
                nextPatrolPointIndex ++;
            }
        }

        if(!movement.isMoving()) {
            planMovement();
        }

        createVisionCone();
    }

    void planMovement() {
        if(patrolPoints.Length == 0)
            return;
        if(nextPatrolPointIndex == patrolPoints.Length)
                nextPatrolPointIndex = 0;

        MovementDirection direction = 0;

        UnityEngine.Vector3 destination = patrolPoints[nextPatrolPointIndex].transform.position;

        float epsilon = 0.0f;

        if(destination.y < this.transform.position.y + epsilon) {
            direction |= MovementDirection.SOUTH;
        } else if (destination.y > this.transform.position.y - epsilon) {
            direction |= MovementDirection.NORTH;
        }

        if(destination.x < this.transform.position.x + epsilon) {
            direction |= MovementDirection.WEST;
        } else if (destination.x > this.transform.position.x - epsilon) {
            direction |= MovementDirection.EAST;
        }

        movement.Move(direction);
    }

    private void createVisionCone() {
        // Raycast leftEdge = Physics2D.Raycast();
        // UnityEngine.Vector3[] verticies = new UnityEngine.Vector3[] {
            
        // };

        MeshFilter visionCone = this.GetComponentInChildren<MeshFilter>();
        var visionConeMesh = visionCone.mesh;
        visionConeMesh.Clear();
        visionConeMesh.uv = new UnityEngine.Vector2[] {
            new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(0, 2f), new UnityEngine.Vector2(2f, 2f)
        };
        Debug.Log(visionConeMesh.uv);
        visionConeMesh.vertices = new UnityEngine.Vector3[] {
            new UnityEngine.Vector3(0, 0, 0), new UnityEngine.Vector3(0, 2f, 0), new UnityEngine.Vector3(2f, 2f, 0)
        };
        Debug.Log(visionConeMesh);
        visionConeMesh.triangles = new int[] { 0, 1, 2 };
    }
}
