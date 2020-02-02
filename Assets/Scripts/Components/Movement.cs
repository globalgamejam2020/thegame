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

        private bool canMove = true;

        void Start() {
            origin = transform.position;
            destination = origin;
        }

        void Update() {
            if (!canMove) return;

            if (isMoving()) Move();
            if (transform.eulerAngles != targetEuler)
            {
                transform.eulerAngles = targetEuler;
                targetEuler = transform.eulerAngles;
            }
        }

        public void CanMove(int canMove) //cuz Unity AnimationEvent dont accept bool param
        {
            this.canMove = System.Convert.ToBoolean(canMove);
        }

        private void Move() {
            float distance = Speed * Time.deltaTime;
            transform.Translate(distance * (destination - origin));
            var closeEnough = (destination - transform.position).sqrMagnitude < 0.01f;
            if (closeEnough) StopMovement();
        }

        private void StopMovement() {
            direction = 0;
            targetEuler = Vector3.zero;
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

        public void Rotate(MovementDirection direction)
        {
            if (direction.Matches(MovementDirection.NORTH))
            {
                if (direction.Matches(MovementDirection.EAST)) targetEuler = new Vector3(0f, 0f, 45f);
                else if (direction.Matches(MovementDirection.WEST)) targetEuler = new Vector3(0f, 0f, -45f);
                else targetEuler = new Vector3(0f, 0f, 0f);
            }
            else if (direction.Matches(MovementDirection.SOUTH))
            {
                if (direction.Matches(MovementDirection.EAST)) targetEuler = new Vector3(0f, 0f, 135f);
                else if (direction.Matches(MovementDirection.WEST)) targetEuler = new Vector3(0f, 0f, -135f);
                else targetEuler = new Vector3(0f, 0f, 180f);
            }
            else if (direction.Matches(MovementDirection.EAST)) targetEuler = new Vector3(0f, 0f, 90f);
            else if (direction.Matches(MovementDirection.WEST)) targetEuler = new Vector3(0f, 0f, -90f);
        }
    }
}