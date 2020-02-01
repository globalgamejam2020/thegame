using System.Collections;
using System.Collections.Generic;
using Component;
using Data;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {

        private Movement movement;

        private void Start() {
            this.movement = GetComponent<Movement>();
        }

        private void Update() {
            MovementDirection direction = 0;

            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            if (vertical > 0) direction |= MovementDirection.NORTH;
            if (vertical < 0) direction |= MovementDirection.SOUTH;
            if (horizontal > 0) direction |= MovementDirection.EAST;
            if (horizontal < 0) direction |= MovementDirection.WEST;

            movement.Move(direction);
        }
    }
}