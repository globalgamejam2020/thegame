using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Component {
    public class AnimationController : MonoBehaviour {
        [SerializeField] private GameObject smallPooPrefab;
        [SerializeField] private GameObject mediumPooPrefab;
        [SerializeField] private GameObject bigPooPrefab;
        
        private Movement movement;
        private Animator animator;

        // Start is called before the first frame update
        void Start() {
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (movement.isMoving()) animator.SetTrigger("Walking");
        }

        public void Turning(bool turning) {
            animator.SetBool("Turning", turning);
        }

        public void Poop() {
            if (movement.isMoving()) return;

            animator.SetTrigger("Poop");
        }

        public void DropPoop() {
            
            
            
            new Poop(transform.position);
            Debug.Log("POOP");
        }

        public void SetTurnDirection(float turnDirection) {
            animator.SetFloat("TurnDirection", turnDirection);
        }
    }
}