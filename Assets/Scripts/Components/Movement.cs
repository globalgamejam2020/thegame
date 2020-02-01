using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed = 0.5f;
        [SerializeField] private Vector2 Position = Vector2.zero;

        private Vector2 Change = Vector2.zero;

        void Start() {
        }

        void Update() {
            if (isMoving()) {
                Position = Position + (Change * Time.deltaTime);
                this.transform.Translate(Position);
                Change -= Change * Time.deltaTime;
                Debug.Log(Change.magnitude);
                if(Change.magnitude < (Change.magnitude * Time.deltaTime)) {
                    Change = Vector2.zero;
                }
            }
        }

        public bool isMoving() {
            return Change != Vector2.zero;
        }

        public Vector2 getChange() {
            return Change;
        }

        public void Move(MovementDirection direction) {
            if (direction.Matches(MovementDirection.NORTH)) {
                Change.y = 1f;
            } else if (direction.Matches(MovementDirection.SOUTH)) {
                Change.y = -1f * Speed;
            }

            if (direction.Matches(MovementDirection.EAST)) {
                Change.x = 1f * Speed;
            } else if (direction.Matches(MovementDirection.WEST)) {
                Change.x = -1f * Speed;
            }
        }
    }
}