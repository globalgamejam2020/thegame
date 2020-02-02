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
        GameObject[] verticies = GameObject.FindGameObjectsWithTag("verticies");

        List<UnityEngine.Vector2> visionConeVector2 = new List<UnityEngine.Vector2> {
            new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(-alertRadius, alertRadius), new UnityEngine.Vector2(alertRadius, alertRadius)
        };

        List<UnityEngine.Vector3> visionConeVector3 = new List<UnityEngine.Vector3> {
            new UnityEngine.Vector3(0, 0, 0), new UnityEngine.Vector3(-alertRadius, alertRadius, 0), new UnityEngine.Vector3(alertRadius, alertRadius, 0)
        };

        RaycastHit2D leftHit = Physics2D.Raycast(new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(-alertRadius, alertRadius), alertRadius);
        if(leftHit.collider != null) {
            Debug.Log(leftHit.point);
            visionConeVector2.Insert(1, leftHit.point);
            visionConeVector2[2] = new UnityEngine.Vector2(leftHit.point.x, alertRadius);
        }
        RaycastHit2D rightHit = Physics2D.Raycast(new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(alertRadius, alertRadius), alertRadius);
        if(rightHit.collider != null) {
            Debug.Log(rightHit.point);
            visionConeVector2.Insert(1, rightHit.point);
            visionConeVector2[2] = new UnityEngine.Vector2(rightHit.point.x, alertRadius);
        }

        MeshFilter visionCone = this.GetComponentInChildren<MeshFilter>();
        var visionConeMesh = visionCone.mesh;
        visionConeMesh.Clear();
        visionConeMesh.vertices = visionConeVector3.ToArray();
        visionConeMesh.uv = visionConeVector2.ToArray();
        visionConeMesh.triangles = new int[] { 0, 1, 2 };
    }
}
