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
        // sort the intersection points in order of their ray's angle
        // connect the dots clockwise

        List<UnityEngine.Vector2> visionConeVector2 = new List<UnityEngine.Vector2> {
            new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(-alertRadius, alertRadius), new UnityEngine.Vector2(alertRadius, alertRadius)
        };

        List<UnityEngine.Vector3> visionConeVector3 = new List<UnityEngine.Vector3> {
            new UnityEngine.Vector3(0, 0, 0), new UnityEngine.Vector3(-alertRadius, alertRadius, 0), new UnityEngine.Vector3(alertRadius, alertRadius, 0)
        };

        List<int> triangles = new List<int> { 0, 1, 2 };

        // RaycastHit2D leftHit = Physics2D.Raycast(new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(-alertRadius, alertRadius), alertRadius);
        // if(leftHit.collider != null) {
        //     Debug.Log("hit");
        //     visionConeVector2.Add(leftHit.point);
        //     visionConeVector2.Add(new UnityEngine.Vector2(leftHit.point.x, alertRadius));
            
        //     visionConeVector3.Add(leftHit.point);
        //     visionConeVector3.Add(new UnityEngine.Vector2(leftHit.point.x, alertRadius));

        //     triangles.Add(0);
        //     triangles.Add(3);
        //     triangles.Add(1);
        // }
        RaycastHit2D rightHit = Physics2D.Raycast(new UnityEngine.Vector2(0, 0), new UnityEngine.Vector2(alertRadius, alertRadius), alertRadius);
        if(rightHit.collider != null) {
            Debug.Log("hit");

            // triangles = new List<int> { 0, 1, 2, 0, 2, 3 };
            triangles = new List<int> { 0, 1, 2};

            visionConeVector2 = new List<UnityEngine.Vector2> {
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(rightHit.point.x, alertRadius),
                new UnityEngine.Vector2(rightHit.point.x, rightHit.point.y)
                
            };

            visionConeVector3 = new List<UnityEngine.Vector3> {
                new UnityEngine.Vector3(0, 0, 0),
                new UnityEngine.Vector3(rightHit.point.x, alertRadius, 0),
                new UnityEngine.Vector3(rightHit.point.x, rightHit.point.y, 0)
                
            };

            // visionConeVector2.Add(rightHit.point);
            // visionConeVector2.Add(new UnityEngine.Vector2(rightHit.point.x, alertRadius));

            // visionConeVector3.Add(rightHit.point);
            // visionConeVector3.Add(new UnityEngine.Vector2(rightHit.point.x, alertRadius));

            // triangles.Add(0);
            // triangles.Add(3);
            // triangles.Add(4);

            // triangles[1] = triangles[4]; //replace what is in pos 1 with what is in pos 4
        }

        Debug.Log("vision cone");
        for(int i = 0; i < visionConeVector3.Count; i++) {
            Debug.Log(visionConeVector3[i]);
        }

        Debug.Log("triangles");
        for(int i = 0; i < triangles.Count; i++) {
            Debug.Log(i + " " + triangles[i] + " " + visionConeVector2[triangles[i]]);
        }

        MeshFilter visionCone = this.GetComponentInChildren<MeshFilter>();
        var visionConeMesh = visionCone.mesh;
        visionConeMesh.Clear();
        visionConeMesh.vertices = visionConeVector3.ToArray();
        visionConeMesh.uv = visionConeVector2.ToArray();
        visionConeMesh.RecalculateNormals();
        visionConeMesh.triangles = triangles.ToArray();
    }
}
