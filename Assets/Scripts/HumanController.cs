using System.Linq;
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
    private AnimationController animationController;

    void Start() {
        movement = GetComponent<Movement>();
        animationController = GetComponent<AnimationController>();
        float nextInvoke = Random.Range(1f, 5f);
        Invoke("Litter", nextInvoke);
    }

    private void Litter()
    {
        animationController.Litter();

        float nextInvoke = Random.Range(1f, 5f);
        Invoke("Litter", nextInvoke);
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

        // UnityEngine.Vector2[] endPoints = new UnityEngine.Vector2[9];
        // List<UnityEngine.Vector2> visionConeVector2 = new List<UnityEngine.Vector2>();
        // List<UnityEngine.Vector3> visionConeVector3 = new List<UnityEngine.Vector3>();
        // List<int> triangles = new List<int>();

        // for(int i = 0; i < 10; i++) {
        //     UnityEngine.Vector2 endPoint;
        //     RaycastHit2D raycast = Physics2D.Raycast(
        //         new UnityEngine.Vector2(0, 0),
        //         new UnityEngine.Vector2(alertRadius, alertRadius),
        //         alertRadius);
        //         if(raycast.collider != null) {
        //             endPoint = raycast.point;
        //         } else {
        //             endPoint = new UnityEngine.Vector2(0,0);
        //         }
        //     endPoints[i] = endPoint;
        // }

        // for(int i = 0; i < 9; i++) {
        //     visionConeVector2.Add(new UnityEngine.Vector2(0, 0));
        //     visionConeVector2.Add(endPoints[i]);
        //     visionConeVector2.Add(endPoints[i+1]);

        //     visionConeVector3.Add(new UnityEngine.Vector3(0, 0, 0));
        //     visionConeVector3.Add(new UnityEngine.Vector3(endPoints[i].x, endPoints[i].y, 0));
        //     visionConeVector3.Add(new UnityEngine.Vector3(endPoints[i+1].x, endPoints[i+1].y, 0));

        //     triangles.Add();
        //     triangles.Add();
        //     triangles.Add();
        // }

        //     MeshFilter visionCone = this.GetComponentInChildren<MeshFilter>();
        //     var visionConeMesh = visionCone.mesh;
        //     visionConeMesh.Clear();
        //     visionConeMesh.vertices = visionConeVector3.ToArray();
        //     visionConeMesh.uv = visionConeVector2.ToArray();
        //     visionConeMesh.RecalculateNormals();
        //     visionConeMesh.triangles = triangles.ToArray();
    }
}
