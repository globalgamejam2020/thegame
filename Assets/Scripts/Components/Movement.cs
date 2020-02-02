using Data;
using Global;
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
            if (isMoving()) {
                var angles = Vector3.zero;
                angles.z = Mathf.LerpAngle(transform.eulerAngles.z, targetEuler.z, 10 * Time.deltaTime);
                transform.localEulerAngles = angles;

                Move();
            }
        }

        public void TurnTo(Transform turnTo)
        {
            targetEuler = turnTo.position - transform.position;
            Rotate();
        }

        private void Rotate()
        {
            transform.up = Vector3.Lerp(transform.up, targetEuler, Time.deltaTime);
            Invoke("Rotate", 0.01f);
        }

        private void Move() {
            var distanceToMove = DistanceToMove();
            transform.Translate(distanceToMove, Space.World);
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

        public void StopMovement() {
            direction = 0;
            targetEuler = Vector3.zero;
            transform.position = destination;
            origin = destination;
            previousDistanceRemaining = 2;
        }

        public void ForceStop()
        {
            direction = 0;
            targetEuler = Vector3.zero;
        }

        public bool isMoving() {
            return direction != 0;
        }

        public MovementDirection GetMovementDirection() {
            return direction;
        }

        public bool Move(MovementDirection direction) {
            if (isMoving()) return false;

            var angles = Vector3.zero;
            this.direction = direction;

            var angleMultiplier = 1f;
            if (direction.Matches(MovementDirection.NORTH)) {
                destination.y += 1f;
                angles.z = 0;
                angleMultiplier = 0.5f;
            } else if (direction.Matches(MovementDirection.SOUTH)) {
                destination.y += -1f;
                angles.z = 180;
                angleMultiplier = 1.5f;
            }

            if (direction.Matches(MovementDirection.EAST)) {
                destination.x += 1f;
                angles.z = -90 * angleMultiplier;
            } else if (direction.Matches(MovementDirection.WEST)) {
                destination.x += -1f;
                angles.z = 90 * angleMultiplier;
            }

            targetEuler = angles;

            if (!GridSystem.Instance.AllowsMovement(destination)) {
                destination = origin;
                this.direction = 0;
                return false;
            }

            return true;
        }
    }
}