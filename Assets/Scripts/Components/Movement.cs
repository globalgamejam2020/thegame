﻿using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;

        private Vector3 origin = Vector3.zero;
        private Vector3 destination = Vector3.zero;

        void Start() {
            origin = transform.position;
            destination = origin;
        }

        void Update() {
            if (isMoving()) {
                float distance = Speed * Time.deltaTime;
                transform.Translate(distance * (destination - origin));
                Debug.Log(destination - origin);
                var closeEnough = (destination - transform.position).sqrMagnitude < 0.1f;
                if(closeEnough) {
                    this.transform.position = destination;
                    origin = destination;
                }
            }
        }

        public bool isMoving() {
            return (destination - origin).sqrMagnitude > 0.1f;
        }

        public void Move(MovementDirection direction) {
            if(isMoving())
                return;
            Debug.Log("moving");
            if (direction.Matches(MovementDirection.NORTH)) {
                destination.y += 1f;
            } else if (direction.Matches(MovementDirection.SOUTH)) {
                destination.y += -1f;
            }

            if (direction.Matches(MovementDirection.EAST)) {
                destination.x += 1f;
            } else if (direction.Matches(MovementDirection.WEST)) {
                destination.x += -1f;
            }
        }
    }
}