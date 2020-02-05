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
        private float overlapRadius;

        private Movement movement;
        private AnimationController animationController;
        private Animator animator;
        private AudioController audioController;

        void Start() {
            movement = GetComponent<Movement>();
            animationController = GetComponent<AnimationController>();
            audioController = GetComponent<AudioController>();
            animator = GetComponent<Animator>();

            float rabidInvoke = Random.Range(10f, 20f);
            Invoke("MakeRabid", rabidInvoke);
            Invoke("MoveRandom", 2f);
        }

        // Update is called once per frame
        void Update() {
            if (rabid && !IsInvoking("Attack")) Invoke("Attack", 0f);
        }

        private void MoveRandom() {
            if (!movement.Move(GetDirection())) movement.Move(RandomMoveDirection());

            float nextInvoke = Random.Range(1f, 5f);
            Invoke("MoveRandom", nextInvoke);

            if (!rabid)
                audioController.PlayFX("move");
            else
                audioController.PlayFX("move2");
        }

        private MovementDirection GetDirection() {
            var treePosition = FindTreeToMoveTo();

            if (ReferenceEquals(treePosition, null)) return RandomMoveDirection();
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

            // Debug.Log($"Found tree! Will move to it: {direction}, {treePosition}, {position}");

            return direction;
        }

        private Vector3? FindTreeToMoveTo() {
            var (closest, distance) = ((GameObject) null, 0f);

            foreach (var direction in new Vector3[] {
                transform.up,
                transform.right,
                transform.right * -1f,
            }) {
                var (obj, d) = FindClosestTree(Physics2D.CircleCastAll(this.transform.position, 5, direction));
                if (obj == null) continue;
                if (closest != null && distance < d) continue;
                closest = obj;
                distance = d;
            }

            if (closest == null || distance < 1f) {
                // animator.SetBool("InTree", true);
                return null;
            } else {
                // animator.SetBool("InTree", false);
                return closest.transform.position;
            }
        }

        private (GameObject, float) FindClosestTree(RaycastHit2D[] hits) {
            var distance = 0f;
            GameObject closest = null;
            foreach (var hit in hits) {
                var obj = hit.transform.gameObject;
                if (!obj.name.Contains("Tree")) continue;

                var thisPosition = this.transform.position;
                var objDistance = (thisPosition - obj.transform.position).sqrMagnitude;

                if (closest == null) {
                    closest = obj;
                    distance = objDistance;
                } else {
                    var closestDistance = (thisPosition - closest.transform.position).sqrMagnitude;
                    if (closestDistance < objDistance && closestDistance > 0f) {
                        closest = obj;
                        distance = closestDistance;
                    }
                }
            }

            return (closest, distance);
        }

        private void Attack() {
            animationController.Attack();

            float nextInvoke = Random.Range(0.5f, 3f);
            Invoke("Attack", nextInvoke);
        }

        private void MakeRabid() {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

            rabid = true;
            animationController.Rabid();
        }

        public bool IsRabid() {
            return rabid;
        }
    }
}