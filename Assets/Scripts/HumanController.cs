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

    private void Litter() {
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

        List<UnityEngine.Vector2> endPoints = new List<UnityEngine.Vector2>();

        GameObject[] vertices = GameObject.FindGameObjectsWithTag("verticies");

        foreach(GameObject vertex in vertices) {
            RaycastHit2D raycast = Physics2D.Raycast(
                // new UnityEngine.Vector2(this.transform.position.x, this.transform.position.y),
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(vertex.transform.position.x, vertex.transform.position.y),
                alertRadius);
                if(raycast.collider != null) {
                    endPoints.Add(new UnityEngine.Vector2(0, 0));
                    endPoints.Add(raycast.point);
                }
        }

        endPoints.Reverse();

        UnityEngine.Vector2[] vertices2 = endPoints.ToArray();

        Triangulator triangulator = new Triangulator(endPoints.ToArray());
        int[] indices = triangulator.Triangulate();
 
        UnityEngine.Vector3[] vertices3 = new UnityEngine.Vector3[vertices2.Length];
        for (int i = 0; i < vertices3.Length; i++) {
            vertices3[i] = new UnityEngine.Vector3(vertices2[i].x, vertices2[i].y, 0);
        }
 
        MeshFilter meshFilter = this.GetComponentInChildren<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Debug.Log(vertices3[0]);

        mesh.Clear();

        mesh.vertices = vertices3;
        mesh.uv = vertices2;
        mesh.triangles = indices;
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        for(int i = 0; i < vertices2.Length; i++) {
            Debug.Log(vertices2[i]);
        }

    }
  }
