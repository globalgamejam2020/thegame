using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;

        private Vector3 origin = Vector3.zero;
        private Vector3 destination = Vector3.zero;

        private MovementDirection direction = 0;

        void Start() {
            origin = transform.position;
            destination = origin;
        }

        void Update() {
            if (isMoving()) Move();
        }

        private void Move() {
            float distance = Speed * Time.deltaTime;
            transform.Translate(distance * (destination - origin));
            var closeEnough = (destination - transform.position).sqrMagnitude < 0.1f;
            if (closeEnough) StopMovement();
        }

        private void StopMovement() {
            direction = 0;
            transform.position = destination;
            origin = destination;
        }

        public bool isMoving() {
            return direction != 0;
        }

        public MovementDirection GetMovementDirection() {
            return direction;
        }

        public void Move(MovementDirection direction) {
            if (isMoving()) return;

            this.direction = direction;
            if (direction.Matches(MovementDirection.NORTH)) destination.y += 1f;
            else if (direction.Matches(MovementDirection.SOUTH)) destination.y += -1f;

            if (direction.Matches(MovementDirection.EAST)) destination.x += 1f;
            else if (direction.Matches(MovementDirection.WEST)) destination.x += -1f;
        }
    }
}