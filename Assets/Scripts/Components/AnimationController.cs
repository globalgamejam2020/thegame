using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Component
{
    public class AnimationController : MonoBehaviour
    {
        public Sprite[] movementSprites; //0,1,2,3 Idle - 4,5 LTurn - 6,7 RTurn - 8,9 Straight
        public Sprite[] actionSprites;

        private Movement movement;
        private SpriteRenderer spriteRenderer;

        public int animationSpeed = 1;

        private MovementStyle movementStyle;
        private MovementDirection[] destination = new MovementDirection[2];

        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<Movement>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            movementStyle = MovementStyle.STRAIGHT;

            destination[0] = MovementDirection.IDLE;
            destination[1] = MovementDirection.IDLE;
        }

        // Update is called once per frame
        void Update()
        {
            Animate();

            if (movement.Change.x > 0)
            {
                destination[0] = MovementDirection.EAST;
            } else if (movement.Change.x < 0)
            {
                destination[0] = MovementDirection.WEST;
            }
            else destination[0] = MovementDirection.IDLE;

            if (movement.Change.y > 0)
            {
                destination[1] = MovementDirection.NORTH;
            }
            else if (movement.Change.y < 0)
            {
                destination[1] = MovementDirection.SOUTH;
            }
            else destination[1] = MovementDirection.IDLE;
        }

        public void Animate()
        {
            int time = Time.frameCount/(20/animationSpeed);

            switch (movementStyle)
            {
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
