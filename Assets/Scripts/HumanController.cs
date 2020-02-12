using System.Net;
using System;
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
        float nextInvoke = UnityEngine.Random.Range(1f, 5f);
        Invoke("Litter", nextInvoke);
    }

    private void Litter() {
        animationController.Litter();

        float nextInvoke = UnityEngine.Random.Range(1f, 5f);
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

        List<UnityEngine.Vector2> endPoints = new List<UnityEngine.Vector2>();
        endPoints.Add(this.transform.position);
        GameObject[] corners = GameObject.FindGameObjectsWithTag("verticies");

        SortedList<float, UnityEngine.Vector2> cornerList = new SortedList<float, UnityEngine.Vector2>();

        foreach(GameObject corner in corners) {
            float polarCoordinateTheta = (float)Math.Atan(System.Convert.ToDouble(corner.transform.position.y / corner.transform.position.x));
            try {
                cornerList.Add(polarCoordinateTheta, new UnityEngine.Vector2(corner.transform.position.x, corner.transform.position.y));
            } catch(ArgumentException exception) {
                cornerList.Add(polarCoordinateTheta + UnityEngine.Random.Range(0.0001f, 0.001f), new UnityEngine.Vector2(corner.transform.position.x, corner.transform.position.y));
            }
        }

        foreach(KeyValuePair<float, UnityEngine.Vector2> corner in cornerList) {
            UnityEngine.Vector2 raycastTarget = new UnityEngine.Vector2(corner.Value.x - this.transform.position.x, corner.Value.y - this.transform.position.y);
            raycastTarget.Normalize();
            RaycastHit2D endPoint = Physics2D.Raycast(this.transform.position, raycastTarget);
            if(endPoint.collider != null) {
                endPoints.Add(endPoint.point);
                Debug.DrawLine(this.transform.position, endPoint.point, Color.red);
            } else {
                Debug.DrawLine(this.transform.position, corner.Value, Color.green);
            }
        }

        UnityEngine.Vector2[] vertices2D = endPoints.ToArray();

        if(vertices2D.Length > 2) {
            for (int i = 1; i < vertices2D.Length; i++) {
                Debug.DrawLine(endPoints[i-1], endPoints[i], Color.blue);
            }
        }

        UnityEngine.Vector3[] vertices3D = new UnityEngine.Vector3[vertices2D.Length];
        for (int i = 0; i < vertices3D.Length; i++) {
            vertices3D[i] = new UnityEngine.Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        Triangulator triangulator = new Triangulator(vertices2D);
        int[] indices = triangulator.Triangulate();

        MeshFilter visionCone = this.GetComponentInChildren<MeshFilter>();
        var visionConeMesh = visionCone.mesh;
        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices3D;
        visionConeMesh.uv = vertices2D;
        visionConeMesh.triangles = indices;

    }
  }
