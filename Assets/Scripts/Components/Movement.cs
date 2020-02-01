using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;
        [SerializeField] private Vector2 Position = Vector2.zero;

        private Vector2? Destination;

        void Start() {
        }

        void Update() {
            if (Destination != null || Destination != Position) {
            }
        }

        public void Move(MovementDirection direction) {
            if (direction.Matches(MovementDirection.NORTH)) {
            } else if (direction.Matches(MovementDirection.SOUTH)) {
            }

            if (direction.Matches(MovementDirection.EAST)) {
            } else if (direction.Matches(MovementDirection.WEST)) {
            }
        }
    }
}