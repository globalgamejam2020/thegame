using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;

        private Vector3 origin = Vector3.zero;
        private Vector3 destination = Vector3.zero;

        private Vector3 targetEuler = Vector3.zero;

        private MovementDirection direction = 0;
        private float previousDistanceRemaining = 2;

        void Start() {
            origin = transform.position;
            destination = origin;
        }

        void Update() {
            if (isMoving()) Move();
            if (transform.eulerAngles != targetEuler) {
                transform.eulerAngles = targetEuler;
                targetEuler = transform.eulerAngles;
            }
        }

        public void CanMove(int canMove) {
            //cuz Unity AnimationEvent dont accept bool param
            enabled = System.Convert.ToBoolean(canMove);
        }

        private void Move() {
            var distanceToMove = DistanceToMove();
            transform.Translate(distanceToMove);
            var distanceRemaining = (destination - transform.position).sqrMagnitude;
            if (distanceRemaining < 0.001f || distanceRemaining > previousDistanceRemaining)
                StopMovement();
            else
                previousDistanceRemaining = distanceRemaining;
        }

        private Vector3 DistanceToMove() {
            var distanceToMove = Speed * Time.deltaTime * (destination - origin);
            distanceToMove.z = 0;

            var finalPosition = transform.position + distanceToMove;

            if (direction.Matches(MovementDirection.NORTH) && finalPosition.y > destination.y)
                distanceToMove.y -= finalPosition.y - destination.y;
            else if (direction.Matches(MovementDirection.SOUTH) && finalPosition.y < destination.y)
                distanceToMove.y += finalPosition.y - destination.y;
            
            if (direction.Matches(MovementDirection.EAST) && finalPosition.x > destination.x)
                distanceToMove.x -= finalPosition.x - destination.x;
            else if (direction.Matches(MovementDirection.WEST) && finalPosition.x < destination.x)
                distanceToMove.x += finalPosition.x - destination.x;

            return distanceToMove;
        }

        private void StopMovement() {
            direction = 0;
            targetEuler = Vector3.zero;
            transform.position = destination;
            origin = destination;
            previousDistanceRemaining = 2;
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
            if (direction.Matches(MovementDirection.NORTH))
                destination.y += 1f;
            else if (direction.Matches(MovementDirection.SOUTH)) destination.y += -1f;

            if (direction.Matches(MovementDirection.EAST))
                destination.x += 1f;
            else if (direction.Matches(MovementDirection.WEST)) destination.x += -1f;
        }

        public void Rotate(MovementDirection direction) {
            if (direction.Matches(MovementDirection.NORTH)) {
                if (direction.Matches(MovementDirection.EAST))
                    targetEuler = new Vector3(0f, 0f, 45f);
                else if (direction.Matches(MovementDirection.WEST))
                    targetEuler = new Vector3(0f, 0f, -45f);
                else
                    targetEuler = new Vector3(0f, 0f, 0f);
            } else if (direction.Matches(MovementDirection.SOUTH)) {
                if (direction.Matches(MovementDirection.EAST))
                    targetEuler = new Vector3(0f, 0f, 135f);
                else if (direction.Matches(MovementDirection.WEST))
                    targetEuler = new Vector3(0f, 0f, -135f);
                else
                    targetEuler = new Vector3(0f, 0f, 180f);
            } else if (direction.Matches(MovementDirection.EAST))
                targetEuler = new Vector3(0f, 0f, 90f);
            else if (direction.Matches(MovementDirection.WEST)) targetEuler = new Vector3(0f, 0f, -90f);
        }
    }
}