using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Component {
    public class AnimationController : MonoBehaviour {
        private Movement movement;
        private Animator animator;

        // Start is called before the first frame update
        void Start() {
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (movement.isMoving() && animator.GetBool("idle") == true) animator.SetBool("idle", false);
            else if (!movement.isMoving() && animator.GetBool("idle") == false) animator.SetBool("idle", true);
        }

        public void Idle(bool idle)
        {
            animator.SetBool("idle", idle);
        }

        public void Turning(bool turning)
        {
            animator.SetBool("idle", turning);
        }

        public void Poop()
        {
            if (movement.isMoving()) return;

            animator.SetTrigger("Poop");
        }

        public void DropPoop()
        {
            new Poop(transform.position);
            Debug.Log("POOP");
        }

        public void SetTurnDirection(float turnDirection)
        {
            animator.SetFloat("TurnDirection", turnDirection);
        }

    }
}