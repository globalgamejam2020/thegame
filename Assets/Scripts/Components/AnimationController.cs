using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Component {
    public class AnimationController : MonoBehaviour {
        public Sprite[] movementSprites; //0,1,2,3 Idle - 4,5 LTurn - 6,7 RTurn - 8,9 Straight
        public Sprite[] actionSprites;

        private Movement movement;
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        public int animationSpeed = 1;

        // Start is called before the first frame update
        void Start() {
            movement = GetComponent<Movement>();
            spriteRenderer = GetComponent<SpriteRenderer>();
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
            animator.SetTrigger("Poop");
        }

        public void SetTurnDirection(float turnDirection)
        {
            animator.SetFloat("TurnDirection", turnDirection);
        }

        public void Animate() {
            int time = Time.frameCount / (20 / animationSpeed);

            Sprite desiredSprite = movementSprites[time];
            spriteRenderer.sprite = desiredSprite;
        }
    }
}