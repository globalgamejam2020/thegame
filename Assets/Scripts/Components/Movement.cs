using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Movement : MonoBehaviour {
        [SerializeField] private float Speed;
        [SerializeField] private Vector2 Position = Vector2.zero;

        private Vector2 Change = Vector2.zero;

        void Start() {
        }

        void Update() {
            if (Change != Vector2.zero) {
            }
        }

        public void Move(MovementDirection direction) {
            if (direction.Matches(MovementDirection.NORTH)) {
                Change.y = 1f;
            } else if (direction.Matches(MovementDirection.SOUTH)) {
                Change.y = -1f;
            }

            if (direction.Matches(MovementDirection.EAST)) {
                Change.x = 1f;
            } else if (direction.Matches(MovementDirection.WEST)) {
                Change.x = -1f;
            }
        }
    }
}