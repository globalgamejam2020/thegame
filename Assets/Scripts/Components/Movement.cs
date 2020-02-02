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
            var closeEnough = (destination - transform.position).sqrMagnitude < 0.01f;
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

        public void Move(Vector2 direction) {
            MovementDirection movementDirection = 0;
            
            if(direction.x > 0)
                movementDirection += 2;
            else if(direction.x < 0)
                movementDirection += 8;

            if(direction.y > 0)
                movementDirection += 1;
            else if(direction.y < 0)
                movementDirection += 4;
            Move(movementDirection);
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