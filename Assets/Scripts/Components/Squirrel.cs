using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Data;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

namespace Component {
    public class Squirrel : MonoBehaviour {
        private bool rabid = false;
        private Movement movement;
        private AnimationController animationController;
        private Animator animator;
        private AudioController audioController;
        // private Tree

        void Start() {
            movement = GetComponent<Movement>();
            animationController = GetComponent<AnimationController>();
            audioController = GetComponent<AudioController>();
            animator = GetComponent<Animator>();
        }

        void Update() {
            
        }

        private void MoveRandom() {
            if (!movement.Move(GetDirection())) {
                movement.Move(RandomMoveDirection());
            }

            if (!rabid)
                audioController.PlayFX("move");
            else
                audioController.PlayFX("move2");
        }

        private MovementDirection GetDirection() {
            var treePosition = FindTreeToMoveTo();

            if (ReferenceEquals(treePosition, null))
                return RandomMoveDirection();
            return DirectionTo((Vector3) treePosition);
        }

        private static MovementDirection RandomMoveDirection() {
            int randv = Random.Range(0, 4);
            int randh = Random.Range(0, 4);
            MovementDirection vertical = (MovementDirection) (1 << randv);
            MovementDirection horizontal = (MovementDirection) (1 << randh);

            return vertical | horizontal;
        }

        private MovementDirection DirectionTo(Vector3 treePosition) {
            MovementDirection direction = 0;
            var position = this.transform.position;

            if (position.y < treePosition.y)
                direction |= MovementDirection.NORTH;
            else if (position.y > treePosition.y) direction |= MovementDirection.SOUTH;

            if (position.x < treePosition.x)
                direction |= MovementDirection.EAST;
            else if (position.x > treePosition.x) direction |= MovementDirection.WEST;

            return direction;
        }

        private Vector3? FindTreeToMoveTo() {
            GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");

            foreach (GameObject tree in trees) {
                RaycastHit2D endPoint = Physics2D.Raycast(this.transform.position, tree.transform.position);
                if(endPoint.collider != null) {
                    
                }
            }
            return null;

            // if (closest == null || distance < 1f) {
            //     animator.SetBool("InTree", true);
            //     return null;
            // } else {
            //     animator.SetBool("InTree", false);
            //     return closest.transform.position;
            // }
        }

        private void Attack() {
            animationController.Attack();
        }

        private void MakeRabid() {
            rabid = true;
            animationController.Rabid();
        }

        public bool IsRabid() {
            return rabid;
        }
    }
}