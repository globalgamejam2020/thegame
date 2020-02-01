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

        public int animationSpeed = 1;

        private MovementStyle movementStyle;

        // Start is called before the first frame update
        void Start() {
            movement = GetComponent<Movement>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            movementStyle = MovementStyle.STRAIGHT;
        }

        // Update is called once per frame
        void Update() {
            Animate();
        }

        public void Animate() {
            int time = Time.frameCount / (20 / animationSpeed);

            switch (movementStyle) {
                case MovementStyle.IDLE:
                    time = time % 4;
                    break;
                case MovementStyle.TURNL:
                    time = (time % 2) + 4;
                    break;
                case MovementStyle.TURNR:
                    time = (time % 2) + 6;
                    break;
                case MovementStyle.STRAIGHT:
                    time = (time % 2) + 8;
                    break;
            }

            Sprite desiredSprite = movementSprites[time];
            spriteRenderer.sprite = desiredSprite;
        }
    }
}