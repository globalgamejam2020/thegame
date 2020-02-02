using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Player;

namespace Component {
    public class AnimationController : MonoBehaviour {
        private CameraShake shake;
        private Movement movement;
        private Animator animator;

        // Start is called before the first frame update
        void Start() {
            
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (movement.isMoving()) {
                animator.SetTrigger("Walking");
            } else {
                animator.SetTrigger("Idle");
            }
        }

        public void Turning(bool turning) {
            animator.SetBool("Turning", turning);
        }

        public void Poop() {
            animator.SetTrigger("Poop");
        }

        public void Litter()
        {
            animator.SetTrigger("Litter");
        }

        public void Rabid() {
            animator.SetTrigger("Rabid");
        }

        public void Attack() {
            animator.SetTrigger("Attack");
        }

        public void DropPoop() {
            // new Poop(transform.position - transform.up * 0.7f);
            new Poop(transform.position - transform.up * 0.7f, Round(transform.position - transform.up));
        }

        private Vector3 Round(Vector3 v) {
            return new Vector3( Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
        }

        public void SetTurnDirection(float turnDirection) {
            animator.SetFloat("TurnDirection", turnDirection);
        }

        public void HORSE()
        {
            animator.SetTrigger("HORSE");
            this.enabled = false;
        }
    }
}