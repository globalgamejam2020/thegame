﻿using System.Collections;
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
            if (movement.isMoving()) animator.SetTrigger("Walking");
        }

        public void Turning(bool turning) {
            animator.SetBool("Turning", turning);
        }

        public void Poop() {
            if (movement.isMoving()) return;

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
            new Poop(transform.position - transform.up * 0.7f);
            Debug.Log("POOP");
        }

        public void SetTurnDirection(float turnDirection) {
            animator.SetFloat("TurnDirection", turnDirection);
        }
    }
}