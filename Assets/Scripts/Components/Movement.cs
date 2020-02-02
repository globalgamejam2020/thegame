using System.Collections;
using System.Collections.Generic;
using Data;
using Global;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;

        private Vector3 origin = Vector3.zero;
        private Vector3 destination = Vector3.zero;

        private MovementDirection direction = 0;
        private MovementDirection next = 0;

        void Start() {
            origin = transform.position;
            destination = origin;
        }

        void Update() {
            if (isMoving()) Move();
        }

        private void Move() {
            float distance = Speed * Time.deltaTime;
            transform.Translate((destination - origin) * distance);
            var closeEnough = (destination - transform.position).sqrMagnitude < 0.001f;
            if (closeEnough) StopMovement();
        }

        private void StopMovement() {
            direction = 0;
            transform.position = destination;
            origin = destination;
            Move(next);
            next = 0;
        }

        public bool isMoving() {
            return direction != 0;
        }

        public float PercentComplete() {
            if (!isMoving()) return 1f;
            return (destination - transform.position).sqrMagnitude / 2;
        }

        public MovementDirection GetMovementDirection() {
            return direction;
        }

        public void Move(MovementDirection direction) {
            if (isMoving()) {
                if (PercentComplete() > 0.75)
                    next = direction;
                return;
            }

            this.direction = direction;
            if (direction.Matches(MovementDirection.NORTH)) destination.y += 1f;
            else if (direction.Matches(MovementDirection.SOUTH)) destination.y += -1f;

            if (direction.Matches(MovementDirection.EAST)) destination.x += 1f;
            else if (direction.Matches(MovementDirection.WEST)) destination.x += -1f;

            if (!GridSystem.Instance.AllowsMovement(destination)) {
                next = 0;
                StopMovement();
            }
        }
    }
}